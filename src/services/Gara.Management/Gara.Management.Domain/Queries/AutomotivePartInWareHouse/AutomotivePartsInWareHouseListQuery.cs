using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.AutomotivePartInWareHouse
{
    public class AutomotivePartsInWarehouseListQuery : IRequest<ServiceResult>
    {
    }

    public class AutomotivePartsInWarehouseListQueryHandler : IRequestHandler<AutomotivePartsInWarehouseListQuery, ServiceResult>
    {
        private readonly IRepository<AutomotivePartInWarehouse> _repository;

        public AutomotivePartsInWarehouseListQueryHandler(IRepository<AutomotivePartInWarehouse> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AutomotivePartsInWarehouseListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var data = await _repository.GetWithIncludeAsync(null, 0, 0, a => a.AutomotivePart);

            result.Success(data);
            return result;
        }
    }
}
