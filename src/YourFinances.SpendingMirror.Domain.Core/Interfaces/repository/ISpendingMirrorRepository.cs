using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFinances.SpendingMirror.Domain.Core.Filters;

namespace YourFinances.SpendingMirror.Domain.Core.Interfaces.repository
{
    public interface ISpendingMirrorRepository
    {
        Task<IEnumerable<Models.SpendingMirror>> GetAllAsync(SpendingMirrorFilter spendingMirrorFilter);
        Task<Models.SpendingMirror> GetByIdAsync(int id, int accountId);
        Task Update(Models.SpendingMirror spendingMirror);
        Task<int> InsertAsync(Models.SpendingMirror spendingMirror);
    }
}
