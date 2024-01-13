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

        public BillListQueryHandler(IRepository<Bill> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(BillListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var bills = await _repository.GetWithIncludeAsync(p => p.Id != null, 0, 0, b => b.Car, b => b.Customer);

            result.Success(bills);

            return result;
        }
    }
}
