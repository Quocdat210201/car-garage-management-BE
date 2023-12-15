using Gara.Management.Domain.Queries.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/car-brand")]
    public class CarBrandController : BaseApiController
    {
        public CarBrandController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetCarBrands(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new CarBrandListQuery(), cancellationToken);

            return Ok(cars);
        }
    }
}
