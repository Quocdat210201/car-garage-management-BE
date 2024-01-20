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

        public NotificationListQueryHandler(IRepository<Notification> notificationRepository,
            IHttpContextAccessor contextAccessor)
        {
            _notificationRepository = notificationRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResult> Handle(NotificationListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var currentUserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notifications = await _notificationRepository.GetWithIncludeAsync(n => n.UserId == Guid.Parse(currentUserId), 0, 0, n => n.Bill, n => n.Bill.Details);

            result.Success(notifications);

            return result;
        }
    }
}
