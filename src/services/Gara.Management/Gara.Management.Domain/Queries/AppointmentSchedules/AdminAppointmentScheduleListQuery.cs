using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.AppointmentSchedules
{
    public class AdminAppointmentScheduleListQuery : IRequest<ServiceResult>
    {
    }

    public class AdminAppointmentScheduleListHandler : IRequestHandler<AdminAppointmentScheduleListQuery, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;

        public AdminAppointmentScheduleListHandler(IRepository<AppointmentSchedule> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(AdminAppointmentScheduleListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var data = await _repository.GetWithIncludeAsync(null, 0, 0, p => p.Staff, p => p.Car, p => p.Car.Owner);

            result.Success(data);
            return result;
        }
    }
}
