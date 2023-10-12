using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Gara.Management.Api.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class BaseApiController : Controller
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
        protected IMapper Mapper => HttpContext.RequestServices.GetRequiredService<IMapper>();
    }
}
