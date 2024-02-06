using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Gara.Extension;

namespace Gara.Management.Domain.Commands.Accounts
{
    public class RegisterAccountCommand : IRequest<ServiceResult>
    {
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        public string? Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Guid? WardId { get; set; }

        public string Role { get; set; }
    }

    public class RegisterAccountHandler : IRequestHandler<RegisterAccountCommand, ServiceResult>
    {
        private readonly UserManager<GaraApplicationUser> _userManager;

        public RegisterAccountHandler(UserManager<GaraApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            request.PhoneNumber = request.PhoneNumber.RemoveAllWhiteSpaces();
            request.Email = request.Email.RemoveAllWhiteSpaces();

            var hasUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            var hasUserByPhoneNumber = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (hasUserByEmail != null || hasUserByPhoneNumber != null)
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "Email hoặc số điện thoại đã được sử dụng" };
                return result;
            }

            var user = new GaraApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = request.PhoneNumber,
                Email = string.IsNullOrEmpty(request.Email) ? null : request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                Address = request.Address,
                WardId = request.WardId
            };

            await _userManager.CreateAsync(user, request.Password);

            await _userManager.AddToRoleAsync(user, request.Role);

            result.Success(user);

            return result;
        }
    }
}
