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

        public NotificationDetailQueryHandler(IRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ServiceResult> Handle(NotificationDetailQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var notification = await _notificationRepository.GetWithIncludeAsync(x => x.Id == request.Id, 0, 0, x => x.Bill, n => n.Bill.Details, n => n.Bill.Car);

            result.Success(notification.FirstOrDefault());

            return result;
        }
    }
}
