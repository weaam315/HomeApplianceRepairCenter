using HomeApplianceRepairCenter.Interfaces;
using HomeApplianceRepairCenter.Models;

namespace HomeApplianceRepairCenter.Services
{
    public class RepairCenter
    {
        // قائمة من الكائنات المشتقة — تستخدم Polymorphism
        private readonly List<Appliance> _appliances;

        public RepairCenter()
        {
            _appliances = new List<Appliance>();
        }

        // دالة ثابتة لحساب عدد الأجهزة حسب الحالة
        public static int GetCountByStatus(IEnumerable<Appliance> appliances, string status)
        {
            return appliances.Count(a => a.Status == status);
        }

        // دالة ثابتة أخرى لحساب متوسط تكلفة الإصلاح
        public static decimal GetAverageRepairCost(IEnumerable<Appliance> appliances)
        {
            var list = appliances.ToList();
            return list.Any() ? list.Average(a => a.CalculateRepairCost()) : 0m;
        }

        public void AddAppliance(Appliance appliance)
        {
            if (appliance == null) throw new ArgumentNullException(nameof(appliance));
            if (!ValidationHelper.IsValidSerialNumber(appliance.SerialNumber))
                throw new ArgumentException("الرقم التسلسلي غير صالح");
            if (!ValidationHelper.IsValidDate(appliance.ReceivedDate))
                throw new ArgumentException("تاريخ الاستلام غير صالح");

            _appliances.Add(appliance);

            // الاشتراك في الأحداث
            appliance.StatusChanged += OnApplianceStatusChanged;      // الحدث العام
            appliance.RepairCompleted += OnRepairCompleted;           // ★ حدث اكتمال الإصلاح
            appliance.DeliveryDelayed += OnDeliveryDelayed;           // ★ حدث تأخر التسليم
        }

        // معالج الحدث العام — يظهر دور التفويض والأحداث
        private void OnApplianceStatusChanged(Appliance sender, string oldStatus, string newStatus)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[إشعار] الجهاز «{sender.DeviceName}» ({sender.SerialNumber}) " +
                              $"تغيّرت حالته من «{oldStatus}» إلى «{newStatus}»");
            Console.ResetColor();
        }

        // ★ معالج حدث اكتمال الإصلاح
        private void OnRepairCompleted(Appliance sender)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[إشعار] ✅ تم إصلاح الجهاز «{sender.DeviceName}» ({sender.SerialNumber}) بنجاح. " +
                              $"التكلفة النهائية: {sender.CalculateRepairCost():N2} ريال");
            Console.ResetColor();
        }

        // ★ معالج حدث تأخر التسليم
        private void OnDeliveryDelayed(Appliance sender)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[تنبيه] ⚠️ الجهاز «{sender.DeviceName}» ({sender.SerialNumber}) " +
                              $"تجاوز مدة الإصلاح المتوقعة (7 أيام)!");
            Console.ResetColor();
        }

        public void DisplayAllAppliances()
        {
            Console.WriteLine("\n=== قائمة الأجهزة في المركز ===");
            foreach (var appliance in _appliances)
            {
                Console.WriteLine(appliance.GetDetails()); // Polymorphism
            }
        }

        public decimal CalculateTotalRepairCost()
        {
            return _appliances.Sum(a => a.CalculateRepairCost()); // Polymorphism
        }
    }
}
