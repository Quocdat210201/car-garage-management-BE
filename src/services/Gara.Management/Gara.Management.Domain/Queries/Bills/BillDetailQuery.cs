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

        public BillDetailQueryHandler(IRepository<Bill> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(BillDetailQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var bill = await _repository.GetWithIncludeAsync(p => p.Id == request.Id, 0, 0, b => b.Car, b => b.Customer);

            result.Success(bill);

            return result;
        }
    }
}
