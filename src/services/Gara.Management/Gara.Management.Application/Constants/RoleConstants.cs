using Gara.Management.Domain.Entities;

namespace Gara.Management.Application.Constants
{
    public static class RoleConstants
    {
        public const string SYSTEM_ADMIN = "System Administrator";
        public const string GARA_ADMIN = "Gara Administrator";
        public const string STAFF = "Staff";
        public const string CUSTOMER = "Customer";

        public static List<GaraApplicationRole> GetListRoles()
        {
            return new List<GaraApplicationRole>() {
                new GaraApplicationRole{
                    Name = SYSTEM_ADMIN,
                    NormalizedName = SYSTEM_ADMIN
                },
                new GaraApplicationRole {
                    Name = GARA_ADMIN,
                    NormalizedName = GARA_ADMIN
                },
                new GaraApplicationRole{
                    Name = STAFF,
                    NormalizedName = STAFF
                },
                new GaraApplicationRole{
                    Name = CUSTOMER,
                    NormalizedName = CUSTOMER
                }
            };
        }
    }
}
