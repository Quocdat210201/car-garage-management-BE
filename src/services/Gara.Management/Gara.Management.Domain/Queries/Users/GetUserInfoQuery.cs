using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Gara.Management.Domain.Queries.Users
{
    public class GetUserInfoQuery : IRequest<ServiceResult>
    {
    }

    public class GetUserRequestHandler : IRequestHandler<GetUserInfoQuery, ServiceResult>
    {
        private readonly UserManager<GaraApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetUserRequestHandler(UserManager<GaraApplicationUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public async Task<ServiceResult> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();

            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRes = _userManager.FindByIdAsync(userId).Result;

            if (userRes == null)
            {
                result.IsSuccess = false;
                return result;
            }

            var userInfo = new UserInfoResponseModel
            {
                Id = userRes.Id,
                Name = userRes.Name,
                Email = userRes.Email,
                PhoneNumber = userRes.PhoneNumber,
                DateOfBirth = userRes.DateOfBirth,
                Address = userRes.Address
            };

            result.Success(userInfo);
            return result;
        }
    }
}
