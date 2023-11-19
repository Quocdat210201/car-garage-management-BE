using Gara.Management.Domain.Queries.Cars;
using Gara.Management.Domain.Storages;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/car")]
    public class CarController : BaseApiController
    {
        private readonly ILogger<CarController> _logger;
        private readonly IGaraStorage _garaStorage;

        public CarController(ILogger<CarController> logger,
            IGaraStorage garaStorage)
        {
            _logger = logger;
            _garaStorage = garaStorage;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new CarListQuery(), cancellationToken);

            return Ok(cars);
        }
    }
}
