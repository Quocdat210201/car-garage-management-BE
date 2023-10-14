using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Cars
{
    public class CarListQuery : IRequest<ServiceResult>
    {
    }

    public class CarListHandler : IRequestHandler<CarListQuery, ServiceResult>
    {
        private readonly IRepository<Car> _repository;

        public CarListHandler(IRepository<Car> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(CarListQuery request, CancellationToken cancellationToken)
        {
            var carList = await _repository.GetAsync();

            ServiceResult result = new();

            result.Success(carList);

            return result;
        }
    }
}
