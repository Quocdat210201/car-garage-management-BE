using Microsoft.AspNetCore.Identity;

namespace Gara.Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }

        public string? Avatar { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }
    }
}
