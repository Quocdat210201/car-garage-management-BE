using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Bills
{
    public class BillListQuery : IRequest<ServiceResult>
    {
    }

    public class BillListQueryHandler : IRequestHandler<BillListQuery, ServiceResult>
    {
        private readonly IRepository<Bill> _repository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartRepository;

        public BillListQueryHandler(IRepository<Bill> repository, IRepository<AutomotivePartInWarehouse> automotivePartRepository)
        {
            _repository = repository;
            _automotivePartRepository = automotivePartRepository;
        }

        public async Task<ServiceResult> Handle(BillListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var bills = await _repository.GetWithIncludeAsync(p => p.Id != null, 0, 0, b => b.Car, b => b.Customer, b => b.Details);

            foreach (var bill in bills)
            {
                if (bill.Details != null)
                    foreach (var detail in bill.Details)
                    {
                        if (detail.AutomotivePartInWarehouseId != null)
                            detail.AutomotivePartInWarehouse = await _automotivePartRepository.GetByIdAsync((Guid)detail.AutomotivePartInWarehouseId);
                    }
            }

            result.Success(bills);

            return result;
        }
    }
}
