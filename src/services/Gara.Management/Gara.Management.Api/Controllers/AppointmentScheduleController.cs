using Gara.Management.Domain.Commands.AppointmentSchedules;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/appointment-schedule")]
    [Consumes("application/json")]
    public class AppointmentScheduleController : BaseApiController
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAppointmentSchedules(CancellationToken cancellationToken)
        //{
        //    var appointmentSchedules = await Mediator.Send(new AppointmentScheduleListQuery(), cancellationToken);

        //    return Ok(appointmentSchedules);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentSchedule([FromBody] CreateAppointmentScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
