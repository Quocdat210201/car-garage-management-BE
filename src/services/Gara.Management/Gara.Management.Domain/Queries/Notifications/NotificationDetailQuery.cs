using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.Notifications
{
    public class NotificationDetailQuery : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        public NotificationDetailQuery(Guid id)
        {
            Id = id;
        }
    }

    public class NotificationDetailQueryHandler : IRequestHandler<NotificationDetailQuery, ServiceResult>
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartInWarehouseRepository;

        public NotificationDetailQueryHandler(IRepository<Notification> notificationRepository, IRepository<AutomotivePartInWarehouse> automotivePartInWarehouseRepository)
        {
            _notificationRepository = notificationRepository;
            _automotivePartInWarehouseRepository = automotivePartInWarehouseRepository;
        }

        public async Task<ServiceResult> Handle(NotificationDetailQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var notifications = await _notificationRepository.GetWithIncludeAsync(x => x.Id == request.Id, 0, 0, x => x.Bill, n => n.Bill.Details, n => n.Bill.Car);

            var notification = notifications.First();

            foreach (var n in notification.Bill.Details)
            {
                var automotivePartInWarehouse = await _automotivePartInWarehouseRepository.GetWithIncludeAsync(x => x.Id == n.AutomotivePartInWarehouseId, 0, 0, x => x.AutomotivePart);

                n.AutomotivePartInWarehouse = automotivePartInWarehouse.First();
            }

            result.Success(notification);
            return result;
        }
    }
}
