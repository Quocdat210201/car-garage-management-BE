using Gara.Management.Application.Data;
using Gara.Management.Application.Repositories;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Repositories;
using Gara.Management.Domain.Services;
using Gara.Management.Domain.Services.AppointmentSchedules;
using Gara.Management.Domain.Services.Factory;
using Gara.Persistance.Abstractions;
using Gara.Persistance.Ef;
using Microsoft.EntityFrameworkCore;

namespace Gara.Management.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GaraManagementDBContent>(options =>
            {
                var connectionString = configuration["Database:ConnectionStrings"];
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // services
            services.AddScoped<IAppointmentScheduleFactory, AppointmentScheduleFactory>();
            services.AddScoped<AdminAppointmentScheduleService, AdminAppointmentScheduleService>();
            services.AddScoped<CustomerAppointmentScheduleService, CustomerAppointmentScheduleService>();

            // repositories
            services.AddScoped<IRepository<Car>, EfRepository<GaraManagementDBContent, Car>>();
            services.AddScoped<IRepository<CarBrand>, EfRepository<GaraManagementDBContent, CarBrand>>();
            services.AddScoped<IRepository<CarType>, EfRepository<GaraManagementDBContent, CarType>>();

            services.AddScoped<IRepository<GoodsDeliveryNote>, EfRepository<GaraManagementDBContent, GoodsDeliveryNote>>();
            services.AddScoped<IRepository<GoodsDeliveryNoteDetail>, EfRepository<GaraManagementDBContent, GoodsDeliveryNoteDetail>>();

            services.AddScoped<IRepository<AutomotivePartSupplier>, EfRepository<GaraManagementDBContent, AutomotivePartSupplier>>();
            services.AddScoped<IRepository<AutomotivePartCategory>, EfRepository<GaraManagementDBContent, AutomotivePartCategory>>();
            services.AddScoped<IRepository<AutomotivePart>, EfRepository<GaraManagementDBContent, AutomotivePart>>();
            services.AddScoped<IRepository<AutomotivePartInWarehouse>, EfRepository<GaraManagementDBContent, AutomotivePartInWarehouse>>();
            services.AddScoped<IRepository<AppointmentScheduleAutomotivePart>, EfRepository<GaraManagementDBContent, AppointmentScheduleAutomotivePart>>();

            services.AddScoped<IRepository<AppointmentSchedule>, EfRepository<GaraManagementDBContent, AppointmentSchedule>>();
            services.AddScoped<IRepository<RepairService>, EfRepository<GaraManagementDBContent, RepairService>>();

            services.AddScoped<IRepository<Ward>, EfRepository<GaraManagementDBContent, Ward>>();
            services.AddScoped<IRepository<District>, EfRepository<GaraManagementDBContent, District>>();

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IAppointmentScheduleRepository, AppointmentScheduleRepository>();
            return services;
        }
    }
}
