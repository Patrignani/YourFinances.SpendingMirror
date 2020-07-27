using System;

namespace YourFinances.SpendingMirror.Domain.Core.Models
{
    public class SpendingMirror
    {
        public int Id { get; set; }
        public string CashFlowGrouping { get; set; }
        public DateTime DateRegister { get; set; }
        public int AccountId { get; set; }
        public double Value { get; set; }
        public string Observation { get; set; }
    }
}
