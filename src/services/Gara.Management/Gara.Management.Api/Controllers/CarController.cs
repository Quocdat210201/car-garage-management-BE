using Gara.Management.Domain.Commands.Cars;
using Gara.Management.Domain.Queries.Cars;
using Gara.Management.Domain.Storages;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/car")]
    public class CarController : BaseApiController
    {
        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger,
            IGaraStorage garaStorage)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new CarListQuery(), cancellationToken);

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(Guid id, CancellationToken cancellationToken)
        {
            var car = await Mediator.Send(new CarDetailQuery(id), cancellationToken);

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarCommand command, CancellationToken cancellationToken)
        {
            var car = await Mediator.Send(command, cancellationToken);

            return Ok(car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(Guid id, [FromBody] UpdateCarCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var car = await Mediator.Send(command, cancellationToken);

            return Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id, CancellationToken cancellationToken)
        {
            var car = await Mediator.Send(new DeleteCarCommand(id), cancellationToken);

            return Ok(car);
        }
    }
}
