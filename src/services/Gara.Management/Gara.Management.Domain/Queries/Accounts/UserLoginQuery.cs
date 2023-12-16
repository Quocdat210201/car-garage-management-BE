using Gara.Domain.ServiceResults;
using Gara.Identity.Domain.Helpers;
using Gara.Management.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Gara.Identity.Domain.MediatR
{
    public class UserLoginQuery : IRequest<ServiceResult>
    {
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

    public class UserLoginHandler : IRequestHandler<UserLoginQuery, ServiceResult>
    {

        private readonly UserManager<GaraApplicationUser> _userManager;
        private readonly SignInManager<GaraApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserLoginHandler(UserManager<GaraApplicationUser> userManager,
            SignInManager<GaraApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ServiceResult> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.PhoneNumber);

            var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordIsCorrect)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Phone number or password is invalid" }
                };
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                };
                claims.AddRange(JwtHelper.GenerateClaims(ClaimTypes.Role, roles.ToList()));

                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                if (claimsPrincipal?.Identity is not ClaimsIdentity claimsIdentity)
                {
                    return new ServiceResult
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = new
                        {
                            access_token = JwtHelper.GenerateJwtToken(claims, _configuration)
                        }
                    };
                }

                claimsIdentity.AddClaims(claims);
                await _signInManager.SignInWithClaimsAsync(user,
                    true,
                    claimsIdentity.Claims);

                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Data = new
                    {
                        access_token = JwtHelper.GenerateJwtToken(claims, _configuration),
                        roles = roles.ToList()
                    }
                };
            }
        }
    }
}
