using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourFinances.SpendingMirror.Domain.Core.DTOs;
using YourFinances.SpendingMirror.Domain.Core.Filters;
using YourFinances.SpendingMirror.Domain.Core.Interfaces;
using YourFinances.SpendingMirror.Domain.Domain.SpendingMirror.Arguments;

namespace YourFinances.SpendingMirror.Domain.Domain.SpendingMirror.Service
{
    public class SpendingMirrorService
    {
        private readonly SessionUser _session;
        private readonly IUnitOfWork _uow;

        public SpendingMirrorService(SessionUser sessionUser, IUnitOfWork uow)
        {
            _session = sessionUser;
            _uow = uow;
        }

        public async Task<IEnumerable<SpendingMirrorResponse>> GetAllAsync(SpendingMirrorFilter spendingMirrorFilter)
        {
            spendingMirrorFilter.AccountId = _session.AccountId;
            var spendingMirrors = await _uow.SpendingMirrorRepository.GetAllAsync(spendingMirrorFilter);
            var cashFlowGrouping = await _uow.CashFlowGroupingRepository
                .GetAllByIdsAsync(spendingMirrors.Select(x => x.CashFlowGrouping).ToList());

            return spendingMirrors.Select(x => new SpendingMirrorResponse
            {
                AccountId = x.AccountId,
                Id = x.Id,
                Observation = x.Observation,
                Value = x.Value,
                CashFlowGrouping = new CashFlowGrouping
                {
                    Id = x.CashFlowGrouping,
                    Identification = cashFlowGrouping.Where(y => y.Id.ToString() == x.CashFlowGrouping)
                    .Select(x => x.Identification).FirstOrDefault(),
                    TypeBox = cashFlowGrouping.Where(y => y.Id.ToString() == x.CashFlowGrouping)
                    .Select(x => x.TypeBox).FirstOrDefault()
                }
            }).ToList();
        }

        public async Task<SpendingMirrorResponse> GetByIdAsync(int id)
        {
            var spendingMirror = await _uow.SpendingMirrorRepository.GetByIdAsync(id, _session.AccountId);
            var cashFlowGrouping = await _uow.CashFlowGroupingRepository.GetByIdsAsync(spendingMirror.CashFlowGrouping);

            return new SpendingMirrorResponse
            {
                Id = spendingMirror.Id,
                AccountId = spendingMirror.AccountId,
                Observation = spendingMirror.Observation,
                Value = spendingMirror.Value,
                CashFlowGrouping = new CashFlowGrouping
                {
                    Id = cashFlowGrouping.Id.ToString(),
                    Identification = cashFlowGrouping.Identification,
                    TypeBox = cashFlowGrouping.TypeBox,

                }
            };
        }

    }
}
