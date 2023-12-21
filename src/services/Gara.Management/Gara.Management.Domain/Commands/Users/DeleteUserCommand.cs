using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gara.Management.Domain.Commands.Users
{
    public class DeleteUserCommand : IRequest<ServiceResult>
    {
        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ServiceResult>
    {
        private readonly UserManager<GaraApplicationUser> _userManager;

        public DeleteUserHandler(UserManager<GaraApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();

            var currentUser = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (currentUser == null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Not found user by id" };
                return result;
            }

            await _userManager.DeleteAsync(currentUser);

            result.Success(null);
            return result;
        }
    }
}
