using Gara.Management.Domain.Queries.Cars;
using Gara.Management.Domain.Queries.CarTypes;
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
        public async Task<IActionResult> GetCarTypes(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new CarTypesListQuery(), cancellationToken);

            return Ok(cars);
        }

        [HttpGet("car-brand")]
        public async Task<IActionResult> GetCarTypesByCarBrand([FromQuery] CarTypeListByCarBrandQuery carTypeListByCarBrandQuery, CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(carTypeListByCarBrandQuery, cancellationToken);

            return Ok(cars);
        }


    }
}
