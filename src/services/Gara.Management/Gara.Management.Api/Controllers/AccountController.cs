using Gara.Domain.ServiceResults;
using Gara.Identity.Domain.MediatR;
using Gara.Management.Api.Controllers;
using Gara.Management.Domain.Commands.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidsWell.Api.Controllers.Common
{
    [Route("api/account")]
    [Consumes("application/json")]
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ServiceResult> Login([FromBody] UserLoginQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return result;
        }

        [HttpPost("sign-up")]
        public async Task<ServiceResult> RegisterAccount([FromBody] RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("verify-token")]
        public async Task<ServiceResult> VerifyToken([FromBody] VerifyTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ServiceResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
