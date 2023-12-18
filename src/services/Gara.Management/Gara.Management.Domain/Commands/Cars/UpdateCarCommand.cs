using Gara.Domain.ServiceResults;
using Gara.Extensions;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Commands.Cars
{
    public class UpdateCarCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public Guid CarTypeId { get; set; }
    }

    public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, ServiceResult>
    {
        private readonly IRepository<Car> _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<CarType> _carTypeRepository;

        public UpdateCarHandler(IRepository<Car> repository,
            IHttpContextAccessor contextAccessor,
            IRepository<CarType> carTypeRepository)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _carTypeRepository = carTypeRepository;
        }

        public async Task<ServiceResult> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            request.RegistrationNumber = request.RegistrationNumber.ToLower().RemoveAllWhiteSpaces();

            ServiceResult result = new();
            var currentCar = await _repository.GetByIdAsync(request.Id);

            if (currentCar == null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Car not found" };
                return result;
            }

            if (currentCar.RegistrationNumber != request.RegistrationNumber)
            {
                if (await _repository.GetWithIncludeAsync(car => car.RegistrationNumber == request.RegistrationNumber) != null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { "Registration number already exists" };
                    return result;
                }
            }

            var carType = await _carTypeRepository.GetByIdAsync(request.CarTypeId);
            if (carType == null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Car type not found" };
                return result;
            }

            await _repository.UpdateAsync(new Car
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                RegistrationNumber = request.RegistrationNumber,
                CarTypeId = request.CarTypeId
            });

            await _repository.SaveChangeAsync();

            result.Success(currentCar);

            return result;
        }
    }
}
