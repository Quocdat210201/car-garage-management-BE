using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Gara.Storage.S3.Api.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
