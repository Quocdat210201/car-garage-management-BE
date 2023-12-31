using Gara.Management.Domain.Queries.AutomotiveParts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/automotive-part")]
    public class AutomotivePartController : BaseApiController
    {
        [HttpGet("category-supplier")]
        public async Task<IActionResult> GetAutomotivePartsByCategoryAndSupplier([FromQuery] AutomotivePartsByCategoryAndSupplierQuery request, CancellationToken cancellationToken)
        {
            var automotiveParts = await Mediator.Send(request, cancellationToken);

            return Ok(automotiveParts);
        }
    }
}
