using Gara.Management.Domain.Queries.Cars;
using Gara.Management.Domain.Queries.Wards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/ward")]
    public class WardController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetWards(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new WardListQuery(), cancellationToken);

            return Ok(cars);
        }

        [HttpGet("district")]
        public async Task<IActionResult> GetWardsByDistrictId([FromQuery] WardListByDistrictQuery wardListByDistrictQuery, CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(wardListByDistrictQuery, cancellationToken);

            return Ok(cars);
        }
    }
}
