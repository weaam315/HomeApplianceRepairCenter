using HomeApplianceRepairCenter.Interfaces;

namespace HomeApplianceRepairCenter.Models
{
    // تعريف التفويض (Delegate) للإشعار بتغير حالة الجهاز
    public delegate void StatusChangedEventHandler(Appliance sender, string oldStatus, string newStatus);

    /// <summary>
    /// فئة مجردة تطبق الواجهة وتحتوي على الخصائص المشتركة
    /// </summary>
    public abstract class Appliance : IRepairable
    {
        // جميع الحقول خاصة (Encapsulation)
        private readonly int _deviceId;
        private string _deviceName;
        private string _status;
        private readonly string _serialNumber;
        private readonly DateTime _receivedDate;

        // الحدث (Event) المرتبط بالتفويض
        public event StatusChangedEventHandler? StatusChanged;

        protected Appliance(int deviceId, string deviceName, string serialNumber, DateTime receivedDate)
        {
            _deviceId = deviceId;
            _deviceName = deviceName;
            _serialNumber = serialNumber;
            _receivedDate = receivedDate;
            _status = "قيد الانتظار";
        }

        // خصائص للقراءة فقط (Read-only Properties)
        public int DeviceId => _deviceId;
        public string DeviceName => _deviceName;
        public string SerialNumber => _serialNumber;   // لا يمكن تعديله بعد الإنشاء
        public DateTime ReceivedDate => _receivedDate;

        // خاصية مع تحقق من القيمة
        public string Status
        {
            get => _status;
            set
            {
                if (_status == value) return;
                string oldStatus = _status;
                _status = value;
                OnStatusChanged(oldStatus, value);
            }
        }

        // إطلاق الحدث
        protected virtual void OnStatusChanged(string oldStatus, string newStatus)
        {
            StatusChanged?.Invoke(this, oldStatus, newStatus);
        }

        // دالة مجردة (Abstract Method) إلزامية
        public abstract decimal CalculateRepairCost();

        public virtual string GetDetails()
        {
            return $"رقم الجهاز: {DeviceId} | الاسم: {DeviceName} | S/N: {SerialNumber} | " +
                   $"الحالة: {Status} | تكلفة الإصلاح: {CalculateRepairCost():N2} ريال";
        }
    }
}