using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Commands.AppointmentSchedules
{
    public class StaffFinishAppointmentScheduleCommand : IRequest<ServiceResult>
    {
        public StaffFinishAppointmentScheduleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class StaffFinishAppointmentScheduleHandler : IRequestHandler<StaffFinishAppointmentScheduleCommand, ServiceResult>
    {
        private readonly IRepository<AppointmentSchedule> _repository;
        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<AppointmentScheduleDetail> _appointmentScheduleDetailRepository;
        private readonly IRepository<Notification> _notificationRepository;

        public StaffFinishAppointmentScheduleHandler(IRepository<AppointmentSchedule> repository, IRepository<AppointmentScheduleDetail> appointmentScheduleDetailRepository, IRepository<Bill> billRepository, IRepository<Notification> notificationRepository)
        {
            _repository = repository;
            _appointmentScheduleDetailRepository = appointmentScheduleDetailRepository;
            _billRepository = billRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<ServiceResult> Handle(StaffFinishAppointmentScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var appointmentSchedules = await _repository.GetWithIncludeAsync(a => a.Id == request.Id, 0, 0, a => a.Car);
            var appointmentSchedule = appointmentSchedules.FirstOrDefault();

            if (appointmentSchedule == null)
            {
                result.ErrorMessages = new List<string> { $"Not found Appointment Schedule by id {request.Id}" };
                return result;
            }

            if (appointmentSchedule.Status != 1)
            {
                result.ErrorMessages = new List<string> { $"Appointment Schedule by id {request.Id} is not in progress" };
                return result;
            }

            appointmentSchedule.Status = 2;
            await _repository.UpdateAsync(appointmentSchedule);

            var bill = new Bill
            {
                Id = Guid.NewGuid(),
                ReceiveCarDate = appointmentSchedule.AppointmentDate,
                ReturnCarDate = DateTime.Now,
                CarId = appointmentSchedule.CarId,
                StaffId = (Guid)appointmentSchedule.StaffId,
                CustomerId = (Guid)appointmentSchedule.Car.OwnerId,
                Total = 0,
                PaymentStatus = 0,
            };

            await _billRepository.AddAsync(bill);

            var appointmentScheduleDetails = await _appointmentScheduleDetailRepository.GetWithIncludeAsync(x => x.AppointmentScheduleId == appointmentSchedule.Id, 0, 0, a => a.RepairService, a => a.AutomotivePartInWarehouse);

            foreach (var appointmentScheduleDetail in appointmentScheduleDetails)
            {
                var price = appointmentScheduleDetail.RepairService.Price;

                if (appointmentScheduleDetail.AutomotivePartInWarehouse != null)
                {
                    price += (appointmentScheduleDetail.AutomotivePartInWarehouse.ReceivePrice * appointmentScheduleDetail.Quantity);
                }

                bill.Total += price;
                appointmentScheduleDetail.BillId = bill.Id;

                await _appointmentScheduleDetailRepository.UpdateAsync(appointmentScheduleDetail);
            }

            await _billRepository.UpdateAsync(bill);

            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Title = "Thông báo Xe của bạn đã sửa xong.",
                Content = $"Thông báo Xe của bạn đã sửa xong. Bạn vui lòng đến cửa hàng thanh toán hóa đơn và nhận xe!",
                IsRead = false,
                UserId = appointmentSchedule.Car.OwnerId,
                BillId = bill.Id
            };

            await _notificationRepository.AddAsync(notification);

            await _repository.SaveChangeAsync();

            result.IsSuccess = true;
            return result;
        }
    }
}
