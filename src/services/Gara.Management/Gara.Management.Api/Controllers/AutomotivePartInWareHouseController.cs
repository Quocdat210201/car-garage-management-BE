using Gara.Management.Api.Constants;
using Gara.Management.Domain.Queries.AutomotivePartInWareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Authorize(Policy = RolePolicy.STAFF_POLICY)]
    [Route("api/automotive-part-in-warehouse")]
    public class AutomotivePartInWarehouseController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAutomotiveParts()
        {
            var result = await Mediator.Send(new AutomotivePartsInWarehouseListQuery());
            return Ok(result);
        }
    }
}
