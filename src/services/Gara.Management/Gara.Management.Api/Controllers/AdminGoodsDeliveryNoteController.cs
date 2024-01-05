using Gara.Management.Api.Constants;
using Gara.Management.Domain.Commands.GoodsDeliverys;
using Gara.Management.Domain.Queries.GoodsDeliverys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/admin-goods-delivery")]
    [Authorize(Policy = RolePolicy.STAFF_POLICY)]
    public class AdminGoodsDeliveryNoteController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllGoodsDeliveryNote()
        {
            var result = await Mediator.Send(new GetGoodsDeliveryNoteListQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoodsDeliveryNote([FromBody] CreateGoodsDeliveryCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
