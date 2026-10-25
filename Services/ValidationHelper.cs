namespace HomeApplianceRepairCenter.Services
{
    /// <summary>
    /// فئة ثابتة للتحقق من صحة البيانات
    /// </summary>
    public static class ValidationHelper
    {
        public static bool IsValidSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber)) return false;
            return serialNumber.Length >= 6 && serialNumber.All(char.IsLetterOrDigit);
        }

        public static bool IsValidDate(DateTime date)
        {
            return date <= DateTime.Now && date >= new DateTime(2000, 1, 1);
        }

        public static bool IsValidDeviceId(int id) => id > 0;

        public static bool IsValidCost(decimal cost) => cost >= 0;
    }
}