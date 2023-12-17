using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Wards
{
    public class WardListByDistrictQuery : IRequest<ServiceResult>
    {
        public Guid DistrictId { get; set; }
    }

    public class WardListByDistrictHandler : IRequestHandler<WardListByDistrictQuery, ServiceResult>
    {
        private readonly IRepository<Ward> _repository;

        public WardListByDistrictHandler(IRepository<Ward> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(WardListByDistrictQuery request, CancellationToken cancellationToken)
        {
            var wardList = await _repository.GetWithIncludeAsync(ward => ward.DistrictId == request.DistrictId);

            ServiceResult result = new();

            result.Success(wardList);

            return result;
        }
    }
}
