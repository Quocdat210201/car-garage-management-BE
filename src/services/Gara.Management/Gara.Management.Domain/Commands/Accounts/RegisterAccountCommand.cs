using Gara.Domain.ServiceResults;
using MediatR;

namespace Gara.Management.Domain.Commands.Accounts
{
    public class RegisterAccountCommand : IRequest<ServiceResult>
    {
    }

    public class RegisterAccountHandler : IRequestHandler<RegisterAccountCommand, ServiceResult>
    {
        public Task<ServiceResult> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
