using Gara.Management.Domain.Queries.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/district")]
    public class DistrictController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetDistricts(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new DistrictListQuery(), cancellationToken);

            return Ok(cars);
        }
    }
}
