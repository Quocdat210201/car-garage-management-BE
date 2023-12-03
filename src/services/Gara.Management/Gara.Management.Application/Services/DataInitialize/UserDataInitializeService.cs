using Gara.Management.Application.Constants;
using Gara.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gara.Management.Application.Services.DataInitialize
{
    public class UserDataInitializeService : IDataInitializeService
    {
        private readonly UserManager<GaraApplicationUser> _userManager;

        public UserDataInitializeService(UserManager<GaraApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public int Order
        {
            get => 2;
            set
            {

            }
        }

        public async Task RunAsync()
        {
            const string password = "123456";

            var systemAdminAccounts = new List<GaraApplicationUser>
                    {
                        new()
                        {
                            UserName = "0111111111",
                            Email = "system.admin@gmail.com",
                            PhoneNumber = "+84111111111",
                            Name = "System Admin"
                        }
                    };

            var garaAdminAccounts = new List<GaraApplicationUser>
                    {
                        new()
                        {
                            UserName = "0211111111",
                            Email = "school.admin@gmail.com",
                            PhoneNumber = "+84211111111",
                            Name = "School Admin"
                        },
                        new()
                        {
                            UserName = "0211111112",
                            Email = "school2.admin@gmail.com",
                            PhoneNumber = "+84211111112",
                            Name = "School Admin 2"
                        }
                    };

            var staffAccounts = new List<GaraApplicationUser>
                    {
                        new()
                        {
                            Id = new System.Guid("92b6f364-e9d0-11ed-a05b-0242ac120003"),
                            UserName = "0311111111",
                            Email = "staff@gmail.com",
                            PhoneNumber = "+84311111111",
                            Name = "staff",
            },
                        new()
                        {
                            UserName = "0988406374",
                            Email = "staff1@gmail.com",
                            PhoneNumber = "+84988406374",
                            Name = "Phuc Vo"
                        }
                    };

            var customerAccounts = new List<GaraApplicationUser>
                    {
                        new()
                        {
                            Id = new System.Guid("FAB5327A-EF7F-49C6-6F76-08DB4BF4B84C"),
                            UserName = "0411111111",
                            Email = "customer@gmail.com",
                            PhoneNumber = "+84411111111",
                            Name = "customer",
                        },
                        new()
                        {
                            UserName = "0935198553",
                            Email = "customer2@gmail.com",
                            PhoneNumber = "+84935198553",
                            Name = "customer2"
                        }
                    };

            foreach (var user in systemAdminAccounts)
            {
                if (_userManager.FindByNameAsync(user.UserName).Result != null) continue;

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.SYSTEM_ADMIN);
                }
            }

            foreach (var user in garaAdminAccounts)
            {
                if (_userManager.FindByNameAsync(user.UserName).Result != null) continue;

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.GARA_ADMIN);
                }
            }

            foreach (var user in staffAccounts)
            {
                if (_userManager.FindByNameAsync(user.UserName).Result != null) continue;

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.STAFF);
                }
            }

            foreach (var user in customerAccounts)
            {
                if (_userManager.FindByNameAsync(user.UserName).Result != null) continue;

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.CUSTOMER);
                }
            }
        }
    }
}