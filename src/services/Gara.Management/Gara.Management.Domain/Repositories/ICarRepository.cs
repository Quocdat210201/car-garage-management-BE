using Gara.Management.Domain.Entities;

namespace Gara.Management.Domain.Repositories
{
    public interface ICarRepository
    {
        public Task<Car> FindCarByRegistrationNumberAsync(string registrationNumber);
    }
}
