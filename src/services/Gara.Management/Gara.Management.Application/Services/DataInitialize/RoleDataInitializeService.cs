using Gara.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gara.Management.Application.Services.DataInitialize
{
    public class RoleDataInitializeService : IDataInitializeService
    {
        private readonly RoleManager<GaraApplicationRole> _roleManager;

        public RoleDataInitializeService(RoleManager<GaraApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public int Order
        {
            get => 1;
            set
            {

            }
        }

        public async Task RunAsync()
        {
            //var listSystemRoles = RoleConstants.GetListRoles();
            //var listRolesInDb = _roleManager.Roles.Select(r => r.Name).ToList();
            //var listRolesNotUsed = listRolesInDb.Except(listSystemRoles.Select(r => r.Name).ToList()).ToList();

            //foreach (var role in listSystemRoles)
            //{
            //    if (await _roleManager.FindByNameAsync(role.Name) == null)
            //    {
            //        await _roleManager.CreateAsync(new GaraApplicationRole(role.Name));
            //    }
            //}

            //if (listRolesNotUsed.Any())
            //{
            //    foreach (var role in listRolesNotUsed)
            //    {
            //        var rolePrepareToDelete = await _roleManager.FindByNameAsync(role);
            //        await _roleManager.DeleteAsync(rolePrepareToDelete);
            //    }
            //}
        }
    }
}
