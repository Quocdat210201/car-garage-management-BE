using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Gara.Management.Domain.Queries.Cars
{
    public class CarListQuery : IRequest<ServiceResult>
    {
    }

    public class CarListHandler : IRequestHandler<CarListQuery, ServiceResult>
    {
        private readonly IRepository<Car> _repository;
        private readonly IHttpContextAccessor _contextAccessor;

        public CarListHandler(IRepository<Car> repository,
            IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResult> Handle(CarListQuery request, CancellationToken cancellationToken)
        {
            var currentUserRole = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            var currentUserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var carList = new List<Car>();
            if (currentUserRole == "Staff" || currentUserRole == "Gara Administrator")
            {
                carList = (List<Car>)await _repository.GetWithIncludeAsync(null, 0, 0, car => car.Owner, c => c.CarType);
            }
            else
            {
                carList = (List<Car>)await _repository.GetWithIncludeAsync(car => car.OwnerId == new Guid(currentUserId), 0, 0);
            }

            ServiceResult result = new();

            result.Success(carList);

            return result;
        }
    }
}
