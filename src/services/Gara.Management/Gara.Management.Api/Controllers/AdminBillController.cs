using Gara.Management.Api.Constants;
using Gara.Management.Domain.Queries.Bills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/admin-bill")]
    [Authorize(Policy = RolePolicy.STAFF_POLICY)]
    public class AdminBillController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBill()
        {
            var result = await Mediator.Send(new BillListQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(Guid id)
        {
            var result = await Mediator.Send(new BillDetailQuery(id));
            return Ok(result);
        }
    }
}
