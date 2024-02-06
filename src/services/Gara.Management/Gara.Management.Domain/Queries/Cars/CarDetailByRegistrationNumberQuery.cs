using Gara.Domain.ServiceResults;
using Gara.Extension;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Queries.Cars
{
    public class CarDetailByRegistrationNumberQuery : IRequest<ServiceResult>
    {
        [Required]
        public string RegistrationNumber { get; set; }

        public CarDetailByRegistrationNumberQuery(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
    }

    public class CarDetailByRegistrationNumberQueryHandler : IRequestHandler<CarDetailByRegistrationNumberQuery, ServiceResult>
    {
        private readonly IRepository<Car> _repository;

        public CarDetailByRegistrationNumberQueryHandler(IRepository<Car> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(CarDetailByRegistrationNumberQuery request, CancellationToken cancellationToken)
        {
            request.RegistrationNumber = request.RegistrationNumber.ToLower().RemoveAllWhiteSpaces();

            var result = new ServiceResult();

            var data = await _repository.GetWithIncludeAsync(x => x.RegistrationNumber == request.RegistrationNumber, 0, 0, x => x.Owner, x => x.CarType, x => x.Bills);

            var car = data.FirstOrDefault();
            if (car == null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Not found car by Registration number" };
                return result;
            }

            result.Success(car);

            return result;
        }
    }
}
