using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Bills
{
    public class BillDetailQuery : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        public BillDetailQuery(Guid id)
        {
            Id = id;
        }
    }

    public class BillDetailQueryHandler : IRequestHandler<BillDetailQuery, ServiceResult>
    {
        private readonly IRepository<Bill> _repository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartRepository;

        public BillDetailQueryHandler(IRepository<Bill> repository, IRepository<AutomotivePartInWarehouse> automotivePartRepository)
        {
            _repository = repository;
            _automotivePartRepository = automotivePartRepository;
        }

        public async Task<ServiceResult> Handle(BillDetailQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var bills = await _repository.GetWithIncludeAsync(p => p.Id == request.Id, 0, 0, b => b.Car, b => b.Customer, b => b.Details);

            var bill = bills.FirstOrDefault();

            if (bill != null && bill.Details != null)
            {
                foreach (var detail in bill.Details)
                {
                    if (detail.AutomotivePartInWarehouseId != null)
                        detail.AutomotivePartInWarehouse = await _automotivePartRepository.GetByIdAsync((Guid)detail.AutomotivePartInWarehouseId);
                }
            }

            result.Success(bill);

            return result;
        }
    }
}
