using Gara.Storage.S3.Domain.Commands.Buckets;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Storage.S3.Api.Controllers
{
    [Route("api/s3-buckets")]
    public class BucketsController : BaseApiController
    {
        //[HttpGet]
        //[Route("buckets")]
        //public async Task<IActionResult> GetBuckets()
        //{
        //    var response = await Mediator.Send(new GetBucketsRequest());
        //    return Ok(response);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateBuckets([FromBody] CreateBucketsCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
