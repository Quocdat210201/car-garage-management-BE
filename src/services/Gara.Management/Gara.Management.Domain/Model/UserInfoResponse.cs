namespace Gara.Management.Domain.Model
{
    public class UserInfoResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<string> Roles { get; set; }
    }
}
