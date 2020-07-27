using Dapper;
using YourFinances.SpendingMirror.Domain.Core.Filters;

namespace YourFinances.SpendingMirror.Infra.Data.Query
{
    public class SpendingMirrorQuery : QueryBasic
    {
        private static string _template = @"SELECT ""Id"", ""CashFlowGrouping"", ""Value"", ""DateRegister"", ""Observation"", ""AccountId""
                            FROM public.""SpendingMirror"" /**where**/  ";
        public static string GetAllScript<T>(SpendingMirrorFilter filter)
        {
            var template = $"{_template} /**orderby**/";

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(template);

            builder.Where(@"""AccountId"" = @AccountId");

            if(!string.IsNullOrEmpty(filter.CashFlowGrouping))
                builder.Where(@"""CashFlowGrouping"" = @CashFlowGrouping");

            if (filter.ValueLarger.HasValue)
                builder.Where(@"""Value"" >= @ValueLarger");

            if (filter.ValueSmaller.HasValue)
                builder.Where(@"""Value"" <= @ValueSmaller");

            if (filter.DateRegisterLarger.HasValue)
                builder.Where(@"""DateRegister"" >= @DateRegisterLarger");

            if (filter.DateRegisterSmaller.HasValue)
                builder.Where(@"""DateRegister"" <= @DateRegisterSmaller");

            if (filter.Ids.Count > 0)
                builder.Where(@"""Id"" in @Ids");

            GetPagination<T>(filter, builder);

            return selector.RawSql;
        }

        public static string GetByIdScript()
        {
            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(_template);

            builder.Where(@"""Id"" = @Id");
            builder.Where(@"""AccountId"" = @AccountId");

            return selector.RawSql;
        }

        public static string UpdateScript()
        {
            var script = @"UPDATE public.""SpendingMirror"" SET 
                         ""CashFlowGrouping"" = @CashFlowGrouping, ""Value"" = @Value, 
                         ""DateRegister"" = @DateRegister, ""Observation"" = @Observation, ""AccountId"" = @AccountId
                         WHERE  ""Id"" = @Id";

            return script;
        }

        public static string InsertScript()
        {
            var script = @"INSERT INTO public.""SpendingMirror""(
	                    ""Id"", ""CashFlowGrouping"", ""Value"", ""DateRegister"", ""Observation"", ""AccountId"")
	                    VALUES (@Id, @CashFlowGrouping, @Value, @DateRegister, @Observation, @AccountId) returning ""Id""";

            return script;
        }
    }
}
