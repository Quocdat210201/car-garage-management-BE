using Gara.Domain.ServiceResults;
using Gara.Extensions;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Enums;
using Gara.Management.Domain.Repositories;
using Gara.Persistance.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Commands.AppointmentSchedules
{
    public class CreateAppointmentScheduleCommandHandler : IRequestHandler<CreateAppointmentScheduleCommand, ServiceResult>
    {
        private readonly UserManager<GaraApplicationUser> _userManager;
        private readonly IRepository<AppointmentSchedule> _appointmentScheduleRepository;
        private readonly ICarRepository _carRepository;
        private readonly IRepository<Car> _efCarRepository;
        private readonly IRepository<AppointmentScheduleDetail> _appointmentScheduleDetailRepository;
        private readonly IRepository<RepairService> _repairServiceRepository;


        public CreateAppointmentScheduleCommandHandler(UserManager<GaraApplicationUser> userManager,
            IRepository<AppointmentSchedule> appointmentScheduleRepository,
            ICarRepository carRepository,
            IRepository<Car> efCarRepository,
            IRepository<AppointmentScheduleDetail> appointmentScheduleDetailRepository,
            IRepository<RepairService> repairServiceRepository)
        {
            _userManager = userManager;
            _appointmentScheduleRepository = appointmentScheduleRepository;
            _carRepository = carRepository;
            _efCarRepository = efCarRepository;
            _appointmentScheduleDetailRepository = appointmentScheduleDetailRepository;
            _repairServiceRepository = repairServiceRepository;
        }

        public async Task<ServiceResult> Handle(CreateAppointmentScheduleCommand request, CancellationToken cancellationToken)
        {
            var serviceResult = new ServiceResult();
            request.PhoneNumber = request.PhoneNumber.RemoveAllWhiteSpaces();
            request.Email = request.Email.RemoveAllWhiteSpaces();

            var user = _userManager.FindByNameAsync(request.PhoneNumber).Result;
            if (user == null)
            {
                user = new GaraApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = request.PhoneNumber,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Name = request.UserName,
                    Address = request.Address,
                    WardId = request.WardId
                };

                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            request.RegistrationNumber = request.RegistrationNumber.RemoveAllWhiteSpaces();

            var car = await _carRepository.FindCarByRegistrationNumberAsync(request.RegistrationNumber);
            if (car == null)
            {
                car = new Car
                {
                    Name = request.RegistrationNumber,
                    RegistrationNumber = request.RegistrationNumber,
                    CarTypeId = request.CarTypeId,
                    OwnerId = user.Id
                };
                await _efCarRepository.AddAsync(car);
            }

            var appointmentSchedule = new AppointmentSchedule
            {
                AppointmentDate = request.AppointmentScheduleDate,
                Content = request.Note,
                Status = 0,
                ReceiveCarAt = request.ReceiveCarAt,
                ReceiveCarAddress = request.Address,
                CarId = car.Id
            };

            await _appointmentScheduleRepository.AddAsync(appointmentSchedule);

            foreach (var repairServiceId in request.RepairServiceIds)
            {
                var repairService = await _repairServiceRepository.GetByIdAsync(repairServiceId);
                if (repairService == null)
                {
                    serviceResult.ErrorMessages.Add($"Not found Repair Service by id {repairServiceId}");
                    return serviceResult;
                }

                var appointmentScheduleDetail = new AppointmentScheduleDetail
                {
                    AppointmentScheduleId = appointmentSchedule.Id,
                    RepairServiceId = repairServiceId
                };

                await _appointmentScheduleDetailRepository.AddAsync(appointmentScheduleDetail);
            }

            await _appointmentScheduleRepository.SaveChangeAsync();
            serviceResult.Success(appointmentSchedule);
            return serviceResult;
        }
    }

    public class CreateAppointmentScheduleCommand : IRequest<ServiceResult>
    {
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public string? Address { get; set; }

        public Guid? WardId { get; set; }

        [Required]
        public DateTime AppointmentScheduleDate { get; set; }

        public string? Note { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public Guid CarTypeId { get; set; }

        public Guid? CarBrandId { get; set; }

        public string? ManufacturingYear { get; set; }

        [Required]
        public List<Guid> RepairServiceIds { get; set; }

        public ReceiveCarAtEnum ReceiveCarAt { get; set; }
    }
}
