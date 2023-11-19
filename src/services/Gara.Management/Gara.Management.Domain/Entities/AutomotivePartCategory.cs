using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class AutomotivePartCategory : EntityBaseWithId
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<AutomotivePart> AutomotiveParts { get; set; }
    }
}
