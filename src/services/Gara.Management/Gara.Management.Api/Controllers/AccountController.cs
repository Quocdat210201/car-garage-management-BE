﻿using Gara.Domain.ServiceResults;
using Gara.Identity.Domain.MediatR;
using Gara.Management.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KidsWell.Api.Controllers.Common
{
    [Route("api/account")]
    [Consumes("application/json")]
    public class AccountController : BaseApiController
    {

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ServiceResult> Login([FromBody] UserLoginRequest request, CancellationToken cancellationToken)
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