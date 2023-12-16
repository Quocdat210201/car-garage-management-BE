using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Cars
{
    public class DistrictListQuery : IRequest<ServiceResult>
    {
    }

    public class DistrictListHandler : IRequestHandler<DistrictListQuery, ServiceResult>
    {
        private readonly IRepository<District> _repository;

        public DistrictListHandler(IRepository<District> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(DistrictListQuery request, CancellationToken cancellationToken)
        {
            var districtList = await _repository.GetAsync();

            ServiceResult result = new();

            result.Success(districtList);

            return result;
        }
    }
}
