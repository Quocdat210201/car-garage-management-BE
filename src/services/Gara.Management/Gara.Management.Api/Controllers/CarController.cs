using Gara.Management.Domain.Queries.Cars;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/car")]
    public class CarController : BaseApiController
    {
        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new CarListQuery(), cancellationToken);

            return Ok(cars);
        }
    }
}
