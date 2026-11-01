using HomeApplianceRepairCenter.Interfaces;

namespace HomeApplianceRepairCenter.Models
{
    // التفويض الأساسي لتغير الحالة (كما هو)
    public delegate void StatusChangedEventHandler(Appliance sender, string oldStatus, string newStatus);

    // تفويض لاكتمال الإصلاح
    public delegate void RepairCompletedEventHandler(Appliance sender);

    // تفويض لتأخر التسليم
    public delegate void DeliveryDelayedEventHandler(Appliance sender);

    public abstract class Appliance : IRepairable
    {
        // جميع الحقول خاصة (Encapsulation)
        private readonly int _deviceId;
        private string _deviceName;
        private string _status;
        private readonly string _serialNumber;
        private readonly DateTime _receivedDate;

        // ★ عدّاد ثابت لكل الأجهزة المنشأة (Static Field)
        private static int _totalCreatedAppliances = 0;

        // ★ خاصية ثابتة للقراءة فقط (Static Property)
        public static int TotalCreatedAppliances => _totalCreatedAppliances;

        // الأحداث
        public event StatusChangedEventHandler? StatusChanged;
        public event RepairCompletedEventHandler? RepairCompleted;
        public event DeliveryDelayedEventHandler? DeliveryDelayed;

        protected Appliance(int deviceId, string deviceName, string serialNumber, DateTime receivedDate)
        {
            _deviceId = deviceId;
            _deviceName = deviceName;
            _serialNumber = serialNumber;
            _receivedDate = receivedDate;
            _status = "قيد الانتظار";

            // ★ زيادة العدّاد الثابت مع كل إنشاء جهاز
            _totalCreatedAppliances++;
        }

        // خصائص للقراءة فقط (Read-only Properties)
        public int DeviceId => _deviceId;
        public string DeviceName => _deviceName;
        public string SerialNumber => _serialNumber;
        public DateTime ReceivedDate => _receivedDate;

        public string Status
        {
            get => _status;
            set
            {
                if (_status == value) return;

                string oldStatus = _status;
                _status = value;

                // الحدث العام (كما هو)
                OnStatusChanged(oldStatus, value);

                // ★ شرط محدد: اكتمال الإصلاح
                if (value == "تم الإصلاح" || value == "مكتمل" || value == "Completed")
                {
                    OnRepairCompleted();
                }

                // ★ شرط محدد: تأخر التسليم (مرت أكثر من 7 أيام والجهاز لم يكتمل)
                if (value != "تم الإصلاح" && value != "مكتمل" && value != "Completed"
                    && (DateTime.Now - _receivedDate).TotalDays > 7)
                {
                    OnDeliveryDelayed();
                }
            }
        }

        protected virtual void OnStatusChanged(string oldStatus, string newStatus)
        {
            StatusChanged?.Invoke(this, oldStatus, newStatus);
        }

        protected virtual void OnRepairCompleted()
        {
            RepairCompleted?.Invoke(this);
        }

        protected virtual void OnDeliveryDelayed()
        {
            DeliveryDelayed?.Invoke(this);
        }

        public abstract decimal CalculateRepairCost();

        public virtual string GetDetails()
        {
            return $"رقم الجهاز: {DeviceId} | الاسم: {DeviceName} | S/N: {SerialNumber} | " +
                   $"الحالة: {Status} | تكلفة الإصلاح: {CalculateRepairCost():N2} ريال";
        }
    }
}
