using Gara.Management.Domain.Queries.AutomotivePartSuppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/automotive-part-supplier")]
    public class AutomotivePartSupplierController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAutomotivePartSuppliers(CancellationToken cancellationToken)
        {
            var automotivePartSuppliers = await Mediator.Send(new AutomotivePartSupplierListQuery(), cancellationToken);

            return Ok(automotivePartSuppliers);
        }
    }
}
