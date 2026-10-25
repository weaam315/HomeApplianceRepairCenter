namespace HomeApplianceRepairCenter.Models
{
    public class Refrigerator : Appliance
    {
        private decimal _gasRefillCost;
        private bool _needsCompressor;

        public Refrigerator(int deviceId, string deviceName, string serialNumber,
                            DateTime receivedDate, decimal gasRefillCost, bool needsCompressor)
            : base(deviceId, deviceName, serialNumber, receivedDate)
        {
            _gasRefillCost = gasRefillCost;
            _needsCompressor = needsCompressor;
        }

        public decimal GasRefillCost
        {
            get => _gasRefillCost;
            set => _gasRefillCost = value >= 0 ? value
                : throw new ArgumentException("التكلفة لا يمكن أن تكون سالبة");
        }

        public bool NeedsCompressor
        {
            get => _needsCompressor;
            set => _needsCompressor = value;
        }

        // إعادة تعريف الدالة المجردة بطريقة خاصة بالثلاجة
        public override decimal CalculateRepairCost()
        {
            decimal cost = 150m; // تكلفة أساسية
            cost += _gasRefillCost;
            if (_needsCompressor) cost += 500m;
            return cost;
        }

        public override string GetDetails()
        {
            return base.GetDetails() +
                   $" | نوع: ثلاجة | تعبئة غاز: {_gasRefillCost:N2} | ضاغط: {(_needsCompressor ? "نعم" : "لا")}";
        }
    }
}