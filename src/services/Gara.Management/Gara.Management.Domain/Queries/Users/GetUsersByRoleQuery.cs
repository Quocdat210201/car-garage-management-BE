using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Model;
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
            List<GaraApplicationUser>? users;

            if (request.Role == null)
            {
                users = await _userManager.Users.ToListAsync(cancellationToken: cancellationToken);
            }
            else
            {
                users = (List<GaraApplicationUser>)await _userManager.GetUsersInRoleAsync(request.Role);
            }
            var data = new List<UserInfoResponseModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userInfo = new UserInfoResponseModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DateOfBirth = user.DateOfBirth,
                    Address = user.Address,
                    Roles = (List<string>)roles,
                };
                data.Add(userInfo);
            }
            result.Success(data);
            return result;
        }
    }
}
