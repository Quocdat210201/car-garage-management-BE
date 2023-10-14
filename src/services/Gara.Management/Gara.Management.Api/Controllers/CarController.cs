using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    public class CarController : BaseApiController
    {
        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> GetCars(CancellationToken cancellationToken)
        {

            return Ok("");
        }
    }
}
