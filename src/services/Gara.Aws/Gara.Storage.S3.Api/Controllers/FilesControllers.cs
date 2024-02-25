using Gara.Storage.S3.Domain.Commands.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Storage.S3.Api.Controllers
{
    [Route("api/s3-files")]
    [AllowAnonymous]
    public class FilesControllers : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateFiles([FromForm] CreateFilesCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
