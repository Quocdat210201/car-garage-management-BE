using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.RepairServices
{
    public class RepairServiceListQuery : IRequest<ServiceResult>
    {
    }

    public class RepairServiceListHandler : IRequestHandler<RepairServiceListQuery, ServiceResult>
    {
        private readonly IRepository<RepairService> _repository;

        public RepairServiceListHandler(IRepository<RepairService> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(RepairServiceListQuery request, CancellationToken cancellationToken)
        {
            var repairServiceList = await _repository.GetAsync();

            ServiceResult result = new();

            result.Success(repairServiceList);

            return result;
        }
    }
}
