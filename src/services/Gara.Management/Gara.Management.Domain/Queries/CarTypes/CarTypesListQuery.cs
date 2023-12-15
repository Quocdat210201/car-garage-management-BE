using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Cars
{
    public class CarTypesListQuery : IRequest<ServiceResult>
    {
    }

    public class CarTypesListQueryHandler : IRequestHandler<CarTypesListQuery, ServiceResult>
    {
        private readonly IRepository<CarType> _repository;

        public CarTypesListQueryHandler(IRepository<CarType> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(CarTypesListQuery request, CancellationToken cancellationToken)
        {
            var carList = await _repository.GetAsync();

            ServiceResult result = new();

            result.Success(carList);

            return result;
        }
    }
}
