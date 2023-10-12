using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class CarType : EntityBaseWithId
    {
        public string TypeName { get; set; }

        public string Description { get; set; }

        public List<Car> Cars { get; set; }
    }
}
