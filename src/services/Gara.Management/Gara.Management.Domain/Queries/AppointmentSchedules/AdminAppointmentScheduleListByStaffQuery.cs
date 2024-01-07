using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;


namespace Gara.Management.Domain.Queries.AppointmentSchedules
{
    public class AdminAppointmentScheduleListByStaffQuery : IRequest<ServiceResult>
    {
        public Guid StaffId { get; set; }

        public AdminAppointmentScheduleListByStaffQuery(Guid staffId)
        {
            StaffId = staffId;
        }
    }

    public class AdminAppointmentScheduleListByStaffHandler : IRequestHandler<AdminAppointmentScheduleListByStaffQuery, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;
        private readonly IRepository<AppointmentScheduleDetail> _appointmentScheduleDetailsRepository;

        public AdminAppointmentScheduleListByStaffHandler(IRepository<AppointmentSchedule> repository,
            IRepository<AppointmentScheduleDetail> appointmentScheduleDetailsRepository)
        {
            _repository = repository;
            _appointmentScheduleDetailsRepository = appointmentScheduleDetailsRepository;
        }

        public async Task<ServiceResult> Handle(AdminAppointmentScheduleListByStaffQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var data = await _repository.GetWithIncludeAsync(p => p.StaffId == request.StaffId, 0, 0, p => p.Staff, p => p.Car, p => p.Car.Owner, p => p.AppointmentScheduleDetails);

            foreach (var item in data)
            {
                item.AppointmentScheduleDetails = (List<AppointmentScheduleDetail>?)await _appointmentScheduleDetailsRepository.GetWithIncludeAsync(p => p.AppointmentScheduleId == item.Id, 0, 0, p => p.RepairService, p => p.AutomotivePartInWarehouse);
            }

            result.Success(data);
            return result;
        }
    }
}
