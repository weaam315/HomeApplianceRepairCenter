namespace HomeApplianceRepairCenter.Models
{
    public class WashingMachine : Appliance
    {
        private decimal _motorRepairCost;
        private bool _needsDrumReplacement;

        public WashingMachine(int deviceId, string deviceName, string serialNumber,
                              DateTime receivedDate, decimal motorRepairCost, bool needsDrumReplacement)
            : base(deviceId, deviceName, serialNumber, receivedDate)
        {
            _motorRepairCost = motorRepairCost;
            _needsDrumReplacement = needsDrumReplacement;
        }

        public decimal MotorRepairCost
        {
            get => _motorRepairCost;
            set => _motorRepairCost = value >= 0 ? value
                : throw new ArgumentException("التكلفة لا يمكن أن تكون سالبة");
        }

        public bool NeedsDrumReplacement
        {
            get => _needsDrumReplacement;
            set => _needsDrumReplacement = value;
        }

        public override decimal CalculateRepairCost()
        {
            decimal cost = 100m; // تكلفة أساسية
            cost += _motorRepairCost;
            if (_needsDrumReplacement) cost += 350m;
            return cost;
        }

        public override string GetDetails()
        {
            return base.GetDetails() +
                   $" | نوع: غسالة | إصلاح محرك: {_motorRepairCost:N2} | حلة: {(_needsDrumReplacement ? "نعم" : "لا")}";
        }
    }
}