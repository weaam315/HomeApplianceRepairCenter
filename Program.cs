using HomeApplianceRepairCenter.Models;
using HomeApplianceRepairCenter.Services;

namespace HomeApplianceRepairCenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var center = new RepairCenter();

            // 1. إنشاء أجهزة مختلفة (الوراثة)
            // ★ جعلت تاريخ استلام الثلاجة أقدم من 7 أيام لتوضيح حدث تأخر التسليم
            var fridge = new Refrigerator(1, "ثلاجة سامسونج", "REF123456", DateTime.Now.AddDays(-10), 80m, true);
            var washer = new WashingMachine(2, "غسالة LG", "WSH789012", DateTime.Now.AddDays(-3), 120m, false);
            var ac = new AirConditioner(3, "مكيف شارب", "AC345678", DateTime.Now.AddDays(-1), 100m, true);

            // ★ عرض الخاصية الثابتة (Static Property)
            Console.WriteLine($"إجمالي الأجهزة التي تم إنشاؤها في النظام: {Appliance.TotalCreatedAppliances}");

            // 2. إضافة الأجهزة للمركز (سيتم الاشتراك بالأحداث الجديدة)
            center.AddAppliance(fridge);
            center.AddAppliance(washer);
            center.AddAppliance(ac);

            // 3. عرض كل الأجهزة (تعدد الأشكال)
            center.DisplayAllAppliances();

            // 4. حساب التكلفة الإجمالية (تعدد الأشكال)
            Console.WriteLine($"\nالتكلفة الإجمالية: {center.CalculateTotalRepairCost():N2} ريال");

            // 5. إطلاق الأحداث عند تغيير الحالة
            Console.WriteLine("\n--- تحديث الحالات ---");

            // ★ الثلاجة قديمة (>7 أيام) → سيُطلق حدث DeliveryDelayed
            fridge.Status = "قيد الإصلاح";

            // ★ الغسالة ستُكمل → سيُطلق حدث RepairCompleted
            washer.Status = "تم الإصلاح";

            ac.Status = "قيد الإصلاح";

            // 6. استخدام الأعضاء الثابتة (Static Members)
            Appliance[] all = { fridge, washer, ac };
            Console.WriteLine($"\nعدد الأجهزة قيد الإصلاح: {RepairCenter.GetCountByStatus(all, "قيد الإصلاح")}");
            Console.WriteLine($"عدد الأجهزة قيد الانتظار: {RepairCenter.GetCountByStatus(all, "قيد الانتظار")}");
            Console.WriteLine($"متوسط تكلفة الإصلاح: {RepairCenter.GetAverageRepairCost(all):N2} ريال");
        }
    }
}
//saber_247296_C4
//weaam_315612_C4
//dima_216011_C1
//ghina_301055_c4
//rahaf_350741 
