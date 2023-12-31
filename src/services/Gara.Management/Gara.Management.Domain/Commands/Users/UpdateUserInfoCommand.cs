using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gara.Management.Domain.Commands.Users
{
    public class UpdateUserInfoCommand : IRequest<ServiceResult>
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string? Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }

    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand, ServiceResult>
    {
        private readonly UserManager<GaraApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UpdateUserInfoCommandHandler(UserManager<GaraApplicationUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public async Task<ServiceResult> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            ServiceResult result = new();

            if (request.Id == null)
            {
                request.Id = new Guid(_contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            var currentUser = _userManager.FindByIdAsync(request.Id.ToString()).Result;
            if (currentUser == null)
            {
                result.IsSuccess = false;
                result.ErrorMessages.Add("Not found user by Id");
                return result;
            }

            var hasUserByEmail = _userManager.FindByEmailAsync(request.Email).Result;
            if (!request.Email.Equals(currentUser.Email) && hasUserByEmail != null)
            {
                result.IsSuccess = false;
                result.ErrorMessages.Add("Email đã được sử dụng");
                return result;
            }

            var hasUserByPhoneNumber = _userManager.FindByNameAsync(request.PhoneNumber).Result;
            if (!request.PhoneNumber.Equals(currentUser.PhoneNumber) && hasUserByPhoneNumber != null)
            {
                result.IsSuccess = false;
                result.ErrorMessages.Add("Số điện thoại đã được sử dụng");
                return result;
            }

            if (currentUser.Name != request.Name)
            {
                currentUser.Name = request.Name;
            }
            if (currentUser.Address != request.Address)
            {
                currentUser.Address = request.Address;
            }
            if (currentUser.Email != request.Email)
            {
                currentUser.Email = request.Email;
                currentUser.NormalizedEmail = request.Email.ToUpper();
            }
            if (currentUser.PhoneNumber != request.PhoneNumber)
            {
                currentUser.PhoneNumber = request.PhoneNumber;
                currentUser.UserName = request.PhoneNumber;
                currentUser.NormalizedUserName = request.PhoneNumber;
            }
            if (currentUser.DateOfBirth != request.DateOfBirth)
            {
                currentUser.DateOfBirth = request.DateOfBirth;
            }

            await _userManager.UpdateAsync(currentUser);

            var userInfo = new UserInfoResponse
            {
                Id = currentUser.Id,
                Name = currentUser.Name,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                DateOfBirth = currentUser.DateOfBirth,
                Address = currentUser.Address
            };

            result.Success(userInfo);
            return result;
        }
    }
}
