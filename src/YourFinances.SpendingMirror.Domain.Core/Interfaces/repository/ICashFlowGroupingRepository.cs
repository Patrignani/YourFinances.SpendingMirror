using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourFinances.SpendingMirror.Domain.Core.Interfaces.repository
{
    public interface ICashFlowGroupingRepository
    {
        Task<IEnumerable<Models.CashFlowGrouping>> GetAllByIdsAsync(IList<string> ids);
        Task<Models.CashFlowGrouping> GetByIdsAsync(string id);
    }
}
