using Gara.Management.Domain.Queries.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/ward")]
    public class WardController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCarBrands(CancellationToken cancellationToken)
        {
            var cars = await Mediator.Send(new WardListQuery(), cancellationToken);

            return Ok(cars);
        }
    }
}
