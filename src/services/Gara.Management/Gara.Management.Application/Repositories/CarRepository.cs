using Gara.Management.Application.Data;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gara.Management.Application.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly GaraManagementDBContent _garaManagementDBContent;
        public CarRepository(GaraManagementDBContent garaManagementDBContent)
        {
            _garaManagementDBContent = garaManagementDBContent;
        }

        public async Task<Car> FindCarByRegistrationNumberAsync(string registrationNumber)
        {
            var car = await _garaManagementDBContent.Cars.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
            if (car == null)
            {
                return null;
            }

            return car;
        }
    }
}
