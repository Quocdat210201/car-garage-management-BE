using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class CarType : EntityBaseWithId
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Car> Cars { get; set; }

        public Guid CarBrandId { get; set; }

        public CarBrand CarBrand { get; set; }
    }
}
