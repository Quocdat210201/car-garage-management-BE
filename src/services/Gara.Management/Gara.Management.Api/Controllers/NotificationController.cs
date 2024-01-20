using Gara.Management.Domain.Queries.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/notification")]
    [Authorize]
    public class NotificationController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetNotifications(CancellationToken cancellationToken)
        {
            var notifications = await Mediator.Send(new NotificationListQuery(), cancellationToken);

            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(Guid id, CancellationToken cancellationToken)
        {
            var notification = await Mediator.Send(new NotificationDetailQuery(id), cancellationToken);

            return Ok(notification);
        }
    }
}
