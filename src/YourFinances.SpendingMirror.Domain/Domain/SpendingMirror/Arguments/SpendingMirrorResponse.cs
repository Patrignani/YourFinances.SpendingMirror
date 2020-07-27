using YourFinances.SpendingMirror.Domain.Core.DTOs;

namespace YourFinances.SpendingMirror.Domain.Domain.SpendingMirror.Arguments
{
    public class SpendingMirrorResponse
    {
        public int Id { get; set; }
        public CashFlowGrouping CashFlowGrouping { get; set; }
        public int AccountId { get; set; }
        public double Value { get; set; }
        public string Observation { get; set; }

    }
}
