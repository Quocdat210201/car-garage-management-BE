using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.RepairServices
{
    public class RepairServiceDetailQuery : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        public RepairServiceDetailQuery(Guid id)
        {
            Id = id;
        }
    }

    public class RepairServiceDetailHandler : IRequestHandler<RepairServiceDetailQuery, ServiceResult>
    {
        private readonly IRepository<RepairService> _repository;

        public RepairServiceDetailHandler(IRepository<RepairService> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(RepairServiceDetailQuery request, CancellationToken cancellationToken)
        {
            var repairServiceList = await _repository.GetByIdAsync(request.Id);

            ServiceResult result = new();

            result.Success(repairServiceList);

            return result;
        }
    }
}
