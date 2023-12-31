using Gara.Management.Domain.Queries.AutomotivePartCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/automotive-part-category")]
    public class AutomotivePartCategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAutomotivePartCategories(CancellationToken cancellationToken)
        {
            var automotivePartCategories = await Mediator.Send(new AutomotivePartCategoryListQuery(), cancellationToken);

            return Ok(automotivePartCategories);
        }
    }
}
