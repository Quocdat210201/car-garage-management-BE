using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Cars
{
    public class CarBrandListQuery : IRequest<ServiceResult>
    {
    }

    public class CarBrandListQueryHandler : IRequestHandler<CarBrandListQuery, ServiceResult>
    {
        private readonly IRepository<CarBrand> _repository;

        public CarBrandListQueryHandler(IRepository<CarBrand> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(CarBrandListQuery request, CancellationToken cancellationToken)
        {
            var carList = await _repository.GetAsync(new List<string> { "CarTypes" });

            ServiceResult result = new();

            result.Success(carList);

            return result;
        }
    }
}
