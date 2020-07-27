namespace YourFinances.SpendingMirror.Domain.Core.Filters
{
    public abstract class FilterBasic
    {
        public FilterBasic(string sort)
        {
            Top = 10;
            Page = 1;
            Sort = sort;
            SortDesc = false;
        }

        public int Top { get; set; }
        public int Page { get; set; }
        public int AccountId { get; set; }
        public string Sort { get; set; }
        public bool SortDesc { get; set; }
        public int Offset => Top * Page; 
    }
}
