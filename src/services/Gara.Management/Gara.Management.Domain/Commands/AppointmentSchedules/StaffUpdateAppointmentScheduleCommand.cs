using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Commands.AppointmentSchedules
{
    public class AutomotivePartsRequest
    {
        public Guid AutomotivePartId { get; set; }

        public int Quantity { get; set; }

    }

    public class AppointmentScheduleDetailRequest
    {
        public List<AutomotivePartsRequest> AutomotivePartsUpdatedRequests { get; set; }

        [Required]
        public Guid RepairServiceId { get; set; }

    }

    public class StaffUpdateAppointmentScheduleCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }

        [Required]
        public List<AppointmentScheduleDetailRequest> RepairServiceUpdateRequests { get; set; }

        public int Status { get; set; }
    }

    public class StaffUpdateAppointmentScheduleHandler : IRequestHandler<StaffUpdateAppointmentScheduleCommand, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;
        private readonly IRepository<AppointmentScheduleDetail> _appointmentScheduleDetailRepository;
        private readonly IRepository<AutomotivePart> _automotivePartRepository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartInWarehouseRepository;

        public StaffUpdateAppointmentScheduleHandler(IRepository<AppointmentSchedule> repository,
            IRepository<AutomotivePart> automotivePartRepository,
            IRepository<AutomotivePartInWarehouse> automotivePartInWarehouseRepository,
            IRepository<AppointmentScheduleDetail> appointmentScheduleDetailRepository)
        {
            _repository = repository;
            _automotivePartRepository = automotivePartRepository;
            _automotivePartInWarehouseRepository = automotivePartInWarehouseRepository;
            _appointmentScheduleDetailRepository = appointmentScheduleDetailRepository;
        }

        public async Task<ServiceResult> Handle(StaffUpdateAppointmentScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var appointmentSchedule = await _repository.GetByIdAsync(request.Id);

            if (appointmentSchedule == null)
            {
                result.ErrorMessages = new List<string> { $"Not found Appointment Schedule by id {request.Id}" };
                return result;
            }

            foreach (var repairServiceUpdateRequest in request.RepairServiceUpdateRequests)
            {
                var repairService = await _automotivePartRepository.GetByIdAsync(repairServiceUpdateRequest.RepairServiceId);
                if (repairService == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages = new List<string> { $"Not found Repair Service by id {repairServiceUpdateRequest.RepairServiceId}" };
                    return result;
                }

                if (repairServiceUpdateRequest.AutomotivePartsUpdatedRequests.Any()) continue;

                foreach (var automotivePartsUpdatedRequest in repairServiceUpdateRequest.AutomotivePartsUpdatedRequests)
                {
                    var automotivePartInWarehouseList = await _automotivePartInWarehouseRepository
                    .ListAsync(a => a.AutomotivePartId == automotivePartsUpdatedRequest.AutomotivePartId);

                    var automotivePartInWarehouse = automotivePartInWarehouseList.FirstOrDefault();

                    if (automotivePartInWarehouse == null)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages = new List<string> { $"Not found Automotive Part by id {automotivePartsUpdatedRequest.AutomotivePartId}" };
                        return result;
                    }

                    if (automotivePartInWarehouse.Quantity < automotivePartsUpdatedRequest.Quantity)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages = new List<string> { $"Automotive Part by id {automotivePartsUpdatedRequest.AutomotivePartId} is not enough" };
                        return result;
                    }

                    automotivePartInWarehouse.Quantity -= automotivePartsUpdatedRequest.Quantity;

                    await _automotivePartInWarehouseRepository.UpdateAsync(automotivePartInWarehouse);

                    var appointmentScheduleDetail = _appointmentScheduleDetailRepository.ListAsync(a => a.AppointmentScheduleId == appointmentSchedule.Id && a.RepairServiceId == repairServiceUpdateRequest.RepairServiceId).Result.FirstOrDefault();

                    if (appointmentScheduleDetail == null)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages = new List<string> { $"Not found Appointment Schedule Detail by appointmentScheduleId {appointmentSchedule.Id} & RepairServiceId {repairServiceUpdateRequest.RepairServiceId}" };
                        return result;
                    }

                    appointmentScheduleDetail.Quantity = automotivePartsUpdatedRequest.Quantity;
                    appointmentScheduleDetail.AutomotivePartInWarehouseId = automotivePartInWarehouse.Id;

                    await _appointmentScheduleDetailRepository.UpdateAsync(appointmentScheduleDetail);
                }
            }

            appointmentSchedule.Status = request.Status;

            await _repository.UpdateAsync(appointmentSchedule);

            await _repository.SaveChangeAsync();

            result.Success(appointmentSchedule);

            return result;
        }
    }
}
