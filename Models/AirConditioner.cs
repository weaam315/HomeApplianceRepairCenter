namespace HomeApplianceRepairCenter.Models
{
    public class AirConditioner : Appliance
    {
        private decimal _freonRefillCost;
        private bool _needsCleaning;

        public AirConditioner(int deviceId, string deviceName, string serialNumber,
                              DateTime receivedDate, decimal freonRefillCost, bool needsCleaning)
            : base(deviceId, deviceName, serialNumber, receivedDate)
        {
            _freonRefillCost = freonRefillCost;
            _needsCleaning = needsCleaning;
        }

        public decimal FreonRefillCost
        {
            get => _freonRefillCost;
            set => _freonRefillCost = value >= 0 ? value
                : throw new ArgumentException("التكلفة لا يمكن أن تكون سالبة");
        }

        public bool NeedsCleaning
        {
            get => _needsCleaning;
            set => _needsCleaning = value;
        }

        public override decimal CalculateRepairCost()
        {
            decimal cost = 200m; // تكلفة أساسية
            cost += _freonRefillCost;
            if (_needsCleaning) cost += 80m;
            return cost;
        }

        public override string GetDetails()
        {
            return base.GetDetails() +
                   $" | نوع: مكيف | تعبئة فريون: {_freonRefillCost:N2} | تنظيف: {(_needsCleaning ? "نعم" : "لا")}";
        }
    }
}