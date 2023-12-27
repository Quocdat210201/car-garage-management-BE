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

        public AdminAppointmentScheduleListByStaffHandler(IRepository<AppointmentSchedule> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AdminAppointmentScheduleListByStaffQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var data = await _repository.GetWithIncludeAsync(p => p.StaffId == request.StaffId, 0, 0, p => p.Staff, p => p.Car, p => p.Car.Owner);

            result.Success(data);
            return result;
        }
    }
}
