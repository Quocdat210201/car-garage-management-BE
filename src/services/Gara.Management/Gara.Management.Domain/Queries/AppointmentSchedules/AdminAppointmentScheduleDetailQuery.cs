using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.AppointmentSchedules
{
    public class AdminAppointmentScheduleDetailQuery : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        public AdminAppointmentScheduleDetailQuery(Guid id)
        {
            this.Id = id;
        }
    }

    public class AdminAppointmentScheduleDetailQueryHandler : IRequestHandler<AdminAppointmentScheduleDetailQuery, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;
        private readonly IRepository<AppointmentScheduleDetail> _appointmentScheduleDetailsRepository;

        public AdminAppointmentScheduleDetailQueryHandler(IRepository<AppointmentSchedule> repository,
            IRepository<AppointmentScheduleDetail> appointmentScheduleDetailsRepository)
        {
            _repository = repository;
            _appointmentScheduleDetailsRepository = appointmentScheduleDetailsRepository;
        }

        public async Task<ServiceResult> Handle(AdminAppointmentScheduleDetailQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var data = await _repository.GetWithIncludeAsync(p => p.Id == request.Id, 0, 0, p => p.Staff, p => p.Car, p => p.Car.Owner, p => p.AppointmentScheduleDetails);

            foreach (var item in data)
            {
                item.AppointmentScheduleDetails = (List<AppointmentScheduleDetail>?)await _appointmentScheduleDetailsRepository.GetWithIncludeAsync(p => p.AppointmentScheduleId == item.Id, 0, 0, p => p.RepairService, p => p.AutomotivePartInWarehouse);
            }

            result.Success(data);
            return result;
        }
    }
}
