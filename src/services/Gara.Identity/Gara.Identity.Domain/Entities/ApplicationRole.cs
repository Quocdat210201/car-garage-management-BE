using Microsoft.AspNetCore.Identity;

namespace Gara.Identity.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;

        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(string rolename) : base(rolename)
        {

        }
    }
}
