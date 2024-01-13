using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.AppointmentSchedules
{
    public class AdminAppointmentScheduleRegistrationNumberQuery : IRequest<ServiceResult>
    {
        public string RegistrationNumber { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public AdminAppointmentScheduleRegistrationNumberQuery(string registrationNumber, DateTime? receiveDate)
        {
            RegistrationNumber = registrationNumber;
            ReceiveDate = receiveDate;
        }
    }

    public class AdminAppointmentScheduleRegistrationNumberHandler : IRequestHandler<AdminAppointmentScheduleRegistrationNumberQuery, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;

        public AdminAppointmentScheduleRegistrationNumberHandler(IRepository<AppointmentSchedule> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AdminAppointmentScheduleRegistrationNumberQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            if (request.ReceiveDate != null)
            {
                var resultByRegistrationAndReceiveDate = await _repository.GetWithIncludeAsync(
                    p => p.Car.RegistrationNumber == request.RegistrationNumber &&
                    DateTime.Compare(p.CreatedOn.Value, request.ReceiveDate.Value) == 0,
                    0, 0, p => p.Staff, p => p.Car, p => p.Car.Owner);

                result.Success(resultByRegistrationAndReceiveDate.FirstOrDefault());

                return result;
            }

            var resultsByRegistration = await _repository.GetWithIncludeAsync(p => p.Car.RegistrationNumber == request.RegistrationNumber, 0, 0, p => p.Staff, p => p.Car, p => p.Car.Owner);

            result.Success(resultsByRegistration);

            return result;
        }
    }
}
