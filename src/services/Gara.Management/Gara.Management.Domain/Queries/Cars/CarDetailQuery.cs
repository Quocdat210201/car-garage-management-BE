using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Gara.Management.Domain.Queries.Cars
{
    public class CarDetailQuery : IRequest<ServiceResult>
    {
        public CarDetailQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class CarDetailHandler : IRequestHandler<CarDetailQuery, ServiceResult>
    {
        private readonly IRepository<Car> _repository;
        private readonly IHttpContextAccessor _contextAccessor;

        public CarDetailHandler(IRepository<Car> repository,
            IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResult> Handle(CarDetailQuery request, CancellationToken cancellationToken)
        {
            var currentUserRole = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            var currentUserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var carList = (List<Car>)await _repository.GetWithIncludeAsync(
                car => car.OwnerId == new Guid(currentUserId) && car.Id == request.Id, 0, 0,
                car => car.Owner, car => car.AppointmentSchedules);

            ServiceResult result = new();

            result.Success(carList);

            return result;
        }
    }
}
