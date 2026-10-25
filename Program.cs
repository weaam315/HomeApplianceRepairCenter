using HomeApplianceRepairCenter.Models;
using HomeApplianceRepairCenter.Services;

namespace HomeApplianceRepairCenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // تفعيل اللغة العربية في الـ Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            var center = new RepairCenter();

            // 1. إنشاء أجهزة مختلفة (الوراثة)
            var fridge = new Refrigerator(1, "ثلاجة سامسونج", "REF123456", DateTime.Now.AddDays(-5), 80m, true);
            var washer = new WashingMachine(2, "غسالة LG", "WSH789012", DateTime.Now.AddDays(-3), 120m, false);
            var ac = new AirConditioner(3, "مكيف شارب", "AC345678", DateTime.Now.AddDays(-1), 100m, true);

            // 2. إضافة الأجهزة للمركز
            center.AddAppliance(fridge);
            center.AddAppliance(washer);
            center.AddAppliance(ac);

            // 3. عرض كل الأجهزة (تعدد الأشكال)
            center.DisplayAllAppliances();
            
            // 4. حساب التكلفة الإجمالية (تعدد الأشكال)
            Console.WriteLine($"\nالتكلفة الإجمالية: {center.CalculateTotalRepairCost():N2} ريال");

            // 5. إطلاق الأحداث عند تغيير الحالة (التفويض والأحداث)
            Console.WriteLine("\n--- تحديث الحالات ---");
            fridge.Status = "قيد الإصلاح";
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
