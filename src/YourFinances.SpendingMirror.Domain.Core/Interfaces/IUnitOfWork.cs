using YourFinances.SpendingMirror.Domain.Core.Interfaces.repository;

namespace YourFinances.SpendingMirror.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ISpendingMirrorRepository SpendingMirrorRepository { get; }
        ICashFlowGroupingRepository CashFlowGroupingRepository { get; }

        void BeginTransaction();
        void RollBack();
        void Commit();
    }
}
