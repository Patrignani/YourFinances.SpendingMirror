using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using YourFinances.SpendingMirror.Domain.Core.Filters;
using YourFinances.SpendingMirror.Domain.Core.Interfaces.repository;
using YourFinances.SpendingMirror.Infra.Data.Query;

namespace YourFinances.SpendingMirror.Infra.Data.Repository
{
    internal class SpendingMirrorRepository : ISpendingMirrorRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public SpendingMirrorRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<Domain.Core.Models.SpendingMirror>> GetAllAsync(SpendingMirrorFilter spendingMirrorFilter)
        {
            return await _connection.QueryAsync<Domain.Core.Models.SpendingMirror>(SpendingMirrorQuery
                .GetAllScript<Domain.Core.Models.SpendingMirror>(spendingMirrorFilter), spendingMirrorFilter, _transaction);
        }

        public async Task<Domain.Core.Models.SpendingMirror> GetByIdAsync(int id, int accountId)
        {
            return await _connection.QueryFirstOrDefaultAsync<Domain.Core.Models.SpendingMirror>(SpendingMirrorQuery
                .GetByIdScript(), new { Id = id, AccountId = accountId }, _transaction);
        }

        public async Task Update(Domain.Core.Models.SpendingMirror spendingMirror)
        {
            await _connection.ExecuteAsync(SpendingMirrorQuery
                .UpdateScript(), spendingMirror, _transaction);
        }

        public async Task<int> InsertAsync(Domain.Core.Models.SpendingMirror spendingMirror)
        {
            return await _connection.ExecuteScalarAsync<int>(SpendingMirrorQuery
                .InsertScript(), spendingMirror, _transaction);
        }
    }
}
