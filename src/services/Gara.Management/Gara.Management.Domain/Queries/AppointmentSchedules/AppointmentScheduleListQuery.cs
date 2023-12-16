using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Services.AppointmentSchedules;
using Gara.Persistance.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Gara.Management.Domain.Queries.AppointmentSchedules
{
    public class AppointmentScheduleListQuery : IRequest<ServiceResult>
    {
    }

    public class AppointmentScheduleListHandler : IRequestHandler<AppointmentScheduleListQuery, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;
        private readonly IAppointmentScheduleFactory _appointmentScheduleFactory;
        private readonly IHttpContextAccessor _contextAccessor;


        public AppointmentScheduleListHandler(IRepository<AppointmentSchedule> repository,
            IAppointmentScheduleFactory appointmentScheduleFactory,
            IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _appointmentScheduleFactory = appointmentScheduleFactory;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResult> Handle(AppointmentScheduleListQuery request, CancellationToken cancellationToken)
        {
            var currentUserRole = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            var currentUserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var _appointmentScheduleService = _appointmentScheduleFactory.get(currentUserRole);

            var appointmentScheduleList = await _appointmentScheduleService.GetAppointmentSchedules(new Guid(currentUserId));

            ServiceResult result = new();

            result.Success(appointmentScheduleList);

            return result;
        }
    }
}
