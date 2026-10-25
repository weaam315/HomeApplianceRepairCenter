namespace HomeApplianceRepairCenter.Interfaces
{
    /// <summary>
    /// واجهة تحدد العمليات الأساسية المشتركة بين جميع الأجهزة
    /// </summary>
    public interface IRepairable
    {
        int DeviceId { get; }
        string DeviceName { get; }
        string Status { get; set; }
        decimal CalculateRepairCost();
        string GetDetails();
    }
}