using Gara.Domain.ServiceResults;
using Gara.Extension;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gara.Management.Domain.Commands.Cars
{
    public class CreateCarCommand : IRequest<ServiceResult>
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public Guid CarTypeId { get; set; }
    }

    public class CreateCarHandler : IRequestHandler<CreateCarCommand, ServiceResult>
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<CarType> _carTypeRepository;

        public CreateCarHandler(IRepository<Car> repository,
            IHttpContextAccessor contextAccessor,
            IRepository<CarType> carTypeRepository)
        {
            _carRepository = repository;
            _contextAccessor = contextAccessor;
            _carTypeRepository = carTypeRepository;
        }

        public async Task<ServiceResult> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            request.RegistrationNumber = request.RegistrationNumber.ToLower().RemoveAllWhiteSpaces();

            ServiceResult result = new();

            if (await _carRepository.GetWithIncludeAsync(c => c.RegistrationNumber == request.RegistrationNumber) != null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Registration number already exists" };
                return result;
            }

            var carType = await _carTypeRepository.GetByIdAsync(request.CarTypeId);
            if (carType == null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Car type not found" };
                return result;
            }

            var currentUserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var car = await _carRepository.AddAsync(new Car
            {
                Name = request.Name,
                Description = request.Description,
                RegistrationNumber = request.RegistrationNumber,
                CarTypeId = request.CarTypeId,
                OwnerId = new Guid(currentUserId)
            });

            await _carRepository.SaveChangeAsync();


            result.Success(car);

            return result;
        }
    }
}
