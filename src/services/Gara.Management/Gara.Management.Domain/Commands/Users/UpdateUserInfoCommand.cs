using Gara.Domain.ServiceResults;
using MediatR;

namespace Gara.Management.Domain.Commands.Users
{
    public class UpdateUserInfoCommand : IRequest<ServiceResult>
    {
    }

    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();

            result.Success(null);

            return result;
        }
    }
}
