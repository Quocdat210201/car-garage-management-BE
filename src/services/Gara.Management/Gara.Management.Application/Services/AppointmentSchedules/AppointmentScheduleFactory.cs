using Gara.Management.Application.Constants;
using Gara.Management.Domain.Services.AppointmentSchedules;
using Microsoft.Extensions.DependencyInjection;

namespace Gara.Management.Domain.Services.Factory
{
    public class AppointmentScheduleFactory : IAppointmentScheduleFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public AppointmentScheduleFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAppointmentScheduleService get(string role)
        {
            return role switch
            {
                RoleConstants.GARA_ADMIN => _serviceProvider.GetService<AdminAppointmentScheduleService>(),
                RoleConstants.STAFF => _serviceProvider.GetService<AdminAppointmentScheduleService>(),
                RoleConstants.CUSTOMER => _serviceProvider.GetService<CustomerAppointmentScheduleService>(),
                _ => _serviceProvider.GetService<CustomerAppointmentScheduleService>(),
            };
        }
    }
}
