using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gara.Management.Domain.Queries.Users
{
    public class GetUsersByRoleQuery : IRequest<ServiceResult>
    {
        public string? Role { get; set; }
    }

    public class StaffListHandler : IRequestHandler<GetUsersByRoleQuery, ServiceResult>
    {
        private readonly UserManager<GaraApplicationUser> _userManager;

        public StaffListHandler(UserManager<GaraApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            if (request.Role == null)
            {
                var users = await _userManager.Users.ToListAsync(cancellationToken: cancellationToken);
                result.Success(users);
            }
            else
            {
                var users = await _userManager.GetUsersInRoleAsync(request.Role);
                result.Success(users);
            }

            return result;
        }
    }
}
