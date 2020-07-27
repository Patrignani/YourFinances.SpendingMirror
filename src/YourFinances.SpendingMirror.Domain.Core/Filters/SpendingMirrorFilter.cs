using System;
using System.Collections.Generic;

namespace YourFinances.SpendingMirror.Domain.Core.Filters
{
    public class SpendingMirrorFilter : FilterBasic
    {
        public SpendingMirrorFilter() : base("Id")
        {
            Ids = new List<int>();
        }

        public IList<int> Ids { get; set; }
        public string CashFlowGrouping { get; set; }
        public DateTime? DateRegisterLarger { get; set; }
        public DateTime? DateRegisterSmaller { get; set; }
        public Double? ValueLarger { get; set; }
        public Double? ValueSmaller { get; set; }
    }
}
