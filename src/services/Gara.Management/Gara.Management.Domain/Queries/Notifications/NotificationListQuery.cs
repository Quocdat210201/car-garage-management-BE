using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Gara.Management.Domain.Queries.Notifications
{
    public class NotificationListQuery : IRequest<ServiceResult>
    {
    }

    public class NotificationListQueryHandler : IRequestHandler<NotificationListQuery, ServiceResult>
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartInWarehouseRepository;

        public NotificationListQueryHandler(IRepository<Notification> notificationRepository,
            IHttpContextAccessor contextAccessor,
            IRepository<AutomotivePartInWarehouse> automotivePartInWarehouseRepository)
        {
            _notificationRepository = notificationRepository;
            _contextAccessor = contextAccessor;
            _automotivePartInWarehouseRepository = automotivePartInWarehouseRepository;
        }

        public async Task<ServiceResult> Handle(NotificationListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var currentUserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notifications = await _notificationRepository.GetWithIncludeAsync(n => n.UserId == Guid.Parse(currentUserId), 0, 0, n => n.Bill, n => n.Bill.Details, n => n.Bill.Car);

            foreach (var n in notifications)
            {
                foreach (var b in n.Bill.Details)
                {
                    var automotivePartInWarehouse = await _automotivePartInWarehouseRepository.GetWithIncludeAsync(x => x.Id == b.AutomotivePartInWarehouseId, 0, 0, x => x.AutomotivePart);

                    b.AutomotivePartInWarehouse = automotivePartInWarehouse.First();
                }
            }


            result.Success(notifications);

            return result;
        }
    }
}
