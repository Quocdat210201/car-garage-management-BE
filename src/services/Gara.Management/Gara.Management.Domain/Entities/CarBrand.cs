using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class CarBrand : EntityBaseWithId
    {
        public string Name { get; set; }

        public List<CarType> CarTypes { get; set; }
    }
}
