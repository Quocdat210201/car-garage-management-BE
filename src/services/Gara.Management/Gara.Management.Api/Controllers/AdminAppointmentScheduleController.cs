﻿using Gara.Management.Api.Constants;
using Gara.Management.Domain.Commands.AppointmentSchedules;
using Gara.Management.Domain.Queries.AppointmentSchedules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gara.Management.Api.Controllers
{
    [Route("api/admin-appointment-schedule")]
    [Authorize(Policy = RolePolicy.STAFF_POLICY)]
    public class AdminAppointmentScheduleController : BaseApiController
    {
        [HttpGet("registration-number")]
        public async Task<IActionResult> GetAppointmentScheduleByRegistrationNumber([FromQuery] AdminAppointmentScheduleRegistrationNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }


        [HttpGet("by-staff/{staffId}")]
        public async Task<IActionResult> GetAppointmentSchedulesByStaff(Guid staffId, CancellationToken cancellationToken)
        {
            var appointmentSchedules = await Mediator.Send(new AdminAppointmentScheduleListByStaffQuery(staffId), cancellationToken);

            return Ok(appointmentSchedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentSchedulesDetail(Guid id, CancellationToken cancellationToken)
        {
            var appointmentSchedules = await Mediator.Send(new AdminAppointmentScheduleDetailQuery(id), cancellationToken);

            return Ok(appointmentSchedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentSchedules(CancellationToken cancellationToken)
        {
            var appointmentSchedules = await Mediator.Send(new AdminAppointmentScheduleListQuery(), cancellationToken);

            return Ok(appointmentSchedules);
        }

        [HttpPut("assign/{id}")]
        public async Task<IActionResult> AssignAppointmentScheduleToStaff(Guid id, [FromBody] AdminAssignAppointmentScheduleToStaffCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("staff-update/{id}")]
        public async Task<IActionResult> StaffUpdateAppointmentSchedule(Guid id, [FromBody] StaffUpdateAppointmentScheduleCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("staff-finish/{id}")]
        public async Task<IActionResult> StaffFinishAppointmentSchedule(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new StaffFinishAppointmentScheduleCommand(id), cancellationToken);

            return Ok(result);
        }
    }
}
