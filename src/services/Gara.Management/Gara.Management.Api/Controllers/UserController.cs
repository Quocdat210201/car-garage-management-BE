using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Authorize]
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        [HttpGet]
        public async Task<ServiceResult> GetUser(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserRequest(), cancellationToken);
            return result;
        }

        //[HttpPost("update-user")]
        //public async Task<ServiceResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await Mediator.Send(request, cancellationToken);
        //    return result;
        //}

        //[HttpPost("change-password")]
        //public async Task<ServiceResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
        //{
        //    var result = await Mediator.Send(request, cancellationToken);
        //    return result;
        //}

        //[HttpPost("logout")]
        //public async Task<ServiceResult> Logout(CancellationToken cancellationToken)
        //{
        //    var result = await Mediator.Send(new LogoutRequest(), cancellationToken);
        //    return result;
        //}
    }
}
