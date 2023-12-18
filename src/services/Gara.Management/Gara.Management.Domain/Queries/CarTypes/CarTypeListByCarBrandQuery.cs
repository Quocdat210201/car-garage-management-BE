using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.CarTypes
{
    public class CarTypeListByCarBrandQuery : IRequest<ServiceResult>
    {
        public Guid CarBrandId { get; set; }
    }

    public class CarTypeListByCarBrandHandler : IRequestHandler<CarTypeListByCarBrandQuery, ServiceResult>
    {
        private readonly IRepository<CarType> _repository;

        public CarTypeListByCarBrandHandler(IRepository<CarType> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(CarTypeListByCarBrandQuery request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();
            var carTypes = await _repository.GetWithIncludeAsync(
                               carType => carType.CarBrandId == request.CarBrandId, 0, 0);

            result.Success(carTypes);

            return result;
        }
    }
}
