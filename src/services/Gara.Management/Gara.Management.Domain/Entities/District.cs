using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class District : EntityBaseWithId
    {
        public string Name { get; set; }

        public LinkedList<Ward> Wards { get; set; }
    }
}
