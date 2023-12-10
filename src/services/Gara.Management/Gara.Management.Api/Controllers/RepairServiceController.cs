using Gara.Management.Domain.Queries.RepairServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/repair-service")]
    public class RepairServiceController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetRepairServices(CancellationToken cancellationToken)
        {
            var repairServices = await Mediator.Send(new RepairServiceListQuery(), cancellationToken);

            return Ok(repairServices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRepairService(Guid id, CancellationToken cancellationToken)
        {
            var repairService = await Mediator.Send(new RepairServiceDetailQuery(id), cancellationToken);

            return Ok(repairService);
        }
    }
}
