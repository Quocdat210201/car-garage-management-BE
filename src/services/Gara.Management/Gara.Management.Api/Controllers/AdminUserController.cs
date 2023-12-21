using Gara.Domain.ServiceResults;
using Gara.Management.Api.Constants;
using Gara.Management.Domain.Commands.Accounts;
using Gara.Management.Domain.Commands.Users;
using Gara.Management.Domain.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/admin-user")]
    [Authorize(Policy = RolePolicy.GARA_ADMIN_POLICY)]
    public class AdminUserController : BaseApiController
    {
        [HttpGet("get-user-by-role")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await Mediator.Send(request, cancellationToken);

            return Ok(users);
        }

        [HttpPut("update-user/{userId}")]
        public async Task<ServiceResult> UpdateUser(Guid userId, [FromBody] UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            request.Id = userId;
            var result = await Mediator.Send(request, cancellationToken);
            return result;
        }

        [HttpPost("create-user")]
        public async Task<ServiceResult> CreateUser([FromBody] RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return result;
        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<ServiceResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteUserCommand(userId), cancellationToken);
            return result;
        }
    }
}
