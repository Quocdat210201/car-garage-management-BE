using Gara.Management.Domain.Queries.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/car-type")]
    public class CarTypeController : BaseApiController
    {
        public CarTypeController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetCarBrands(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new CarTypesListQuery(), cancellationToken);

            return Ok(cars);
        }
    }
}
