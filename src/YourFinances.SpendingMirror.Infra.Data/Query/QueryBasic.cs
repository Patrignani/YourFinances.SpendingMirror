using Dapper;
using YourFinances.SpendingMirror.Domain.Core.Filters;

namespace YourFinances.SpendingMirror.Infra.Data.Query
{
    public abstract class QueryBasic
    {
        public static void GetPagination<T>(FilterBasic filter, SqlBuilder builder)
        {
            var sort = filter.Sort;
            RemoveInjectorSort<T>(ref sort);
            var desc = filter.SortDesc ? " DESC " : " ";
            var script = $@" ""{sort}"" {desc} LIMIT  {filter.Top }  OFFSET {filter.Offset }";
            builder.OrderBy(script);
        }

        private static void RemoveInjectorSort<T>(ref string sort)
        {
            bool valid = false;
            foreach (var prop in typeof(T).GetProperties())
            {
                if (sort.ToLower() == prop.Name.ToLower())
                    valid = true;
            }

            if (!valid)
                sort = @"""Id""";
        }
    }
}
