using Gara.Management.Domain.Queries.AutomotiveParts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/automotive-part")]
    public class AutomotivePartController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAutomotiveParts()
        {
            var result = await Mediator.Send(new AutomotivePartsListQuery());
            return Ok(result);
        }

        [HttpGet("category-supplier")]
        public async Task<IActionResult> GetAutomotivePartsByCategoryAndSupplier([FromQuery] AutomotivePartsByCategoryAndSupplierQuery request, CancellationToken cancellationToken)
        {
            var automotiveParts = await Mediator.Send(request, cancellationToken);

            return Ok(automotiveParts);
        }
    }
}
