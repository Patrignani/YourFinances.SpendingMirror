using MongoDB.Driver;
using System.Data;
using YourFinances.SpendingMirror.Domain.Core.Interfaces;
using YourFinances.SpendingMirror.Domain.Core.Interfaces.repository;
using YourFinances.SpendingMirror.Infra.Data.Repository;

namespace YourFinances.SpendingMirror.Infra.Data
{
    internal class UnitOfWork : IUnitOfWork 
    {
        private readonly IDbConnection _connection;
        private readonly IMongoDatabase _mongoDatabase;
        private IDbTransaction _transaction;
        private ISpendingMirrorRepository _spendingMirrorRepository;
        private ICashFlowGroupingRepository _cashFlowGroupingRepository;
    

        public UnitOfWork(IDbConnection connection, IMongoDatabase mongoDatabase)
        {
            _connection = connection;
            _mongoDatabase = mongoDatabase;
        }

        public void BeginTransaction() => _transaction = _connection.BeginTransaction();
        public void RollBack() => _transaction.Rollback();
        public void Commit() => _transaction.Commit();

        public ISpendingMirrorRepository SpendingMirrorRepository
        {
            get
            {
                if (_spendingMirrorRepository == null)
                    _spendingMirrorRepository = new SpendingMirrorRepository(_connection, _transaction);

                return _spendingMirrorRepository;
            }
        }

        public ICashFlowGroupingRepository CashFlowGroupingRepository
        {
            get
            {
                if (_cashFlowGroupingRepository == null)
                    _cashFlowGroupingRepository = new CashFlowGroupingRepository(_mongoDatabase);

                return _cashFlowGroupingRepository;
            }
        }
    }
}
