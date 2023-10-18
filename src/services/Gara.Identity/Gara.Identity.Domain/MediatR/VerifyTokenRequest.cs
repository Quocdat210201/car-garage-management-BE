using Gara.Domain.ServiceResults;
using Gara.Identity.Domain.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Gara.Identity.Domain.MediatR
{
    public class VerifyTokenRequest : IRequest<ServiceResult>
    {
        [Required]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

    public class VerifyTokenHandler : IRequestHandler<VerifyTokenRequest, ServiceResult>
    {
        private readonly IConfiguration _configuration;

        public VerifyTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ServiceResult> Handle(VerifyTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await Task.Run(() => JwtHelper.ValidateJwtToken(request.AccessToken, _configuration), cancellationToken);
            if (result == true)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = request
                };
            }
            else
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string>() { "Token is invalid" }
                };
            }
        }
    }
}
