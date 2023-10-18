using Gara.Domain.ServiceResults;
using MediatR;

namespace Gara.Identity.Domain.MediatR
{
    public class ForgotPasswordRequest : IRequest<ServiceResult>
    {
        public string PhoneNumber { get; set; }
    }
}
