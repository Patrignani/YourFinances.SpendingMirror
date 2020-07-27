using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourFinances.SpendingMirror.Domain.Core.Interfaces.repository;

namespace YourFinances.SpendingMirror.Infra.Data.Repository
{
    public class CashFlowGroupingRepository : ICashFlowGroupingRepository
    {
        private readonly IMongoDatabase _database;
        public CashFlowGroupingRepository(IMongoDatabase database)
        {
            _database = database;
           
        }

        public async Task<IEnumerable<Domain.Core.Models.CashFlowGrouping>> GetAllByIdsAsync(IList<string> ids)
        {
            var collection = _database.GetCollection<Domain.Core.Models.CashFlowGrouping>("cashflowgroupings"); ;
          
            var filter = Builders<Domain.Core.Models.CashFlowGrouping>.Filter
                .In(p => p.Id, ids.Select(x => ObjectId.Parse(x)));
            return  (await collection.FindAsync<Domain.Core.Models.CashFlowGrouping>(filter)).ToList();
        }

        public async Task<Domain.Core.Models.CashFlowGrouping> GetByIdsAsync(string id)
        {
            var collection = _database.GetCollection<Domain.Core.Models.CashFlowGrouping>("cashflowgroupings"); ;
            return (await collection.FindAsync(x => x.Id == ObjectId.Parse(id))).FirstOrDefault();
        
        }
    }
}
