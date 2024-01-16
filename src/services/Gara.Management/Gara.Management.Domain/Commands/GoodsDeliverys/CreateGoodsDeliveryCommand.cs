using Gara.Domain.ServiceResults;
using Gara.Extensions;
using Gara.Management.Domain.Entities;
using Gara.Management.Domain.Model;
using Gara.Persistance.Abstractions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Gara.Management.Domain.Commands.GoodsDeliverys
{
    public class CreateGoodsDeliveryCommand : IRequest<ServiceResult>
    {
        [Required]
        public string GoodsDeliveryCode { get; set; }

        public DateTime ReceiveDate { get; set; }

        public Guid StaffId { get; set; }

        public List<GoodsDeliveryNoteDetailRequest> GoodsDeliveryNoteDetails { get; set; }
    }

    public class CreateGoodsDeliveryCommandHandler : IRequestHandler<CreateGoodsDeliveryCommand, ServiceResult>
    {
        private readonly IRepository<AutomotivePart> _automotivePartRepository;
        private readonly IRepository<GoodsDeliveryNote> _goodsDeliveryNoteRepository;
        private readonly IRepository<GoodsDeliveryNoteDetail> _goodsDeliveryNoteDetailRepository;
        private readonly IRepository<AutomotivePartSupplier> _automotivePartSupplierRepository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartInWarehouseRepository;

        public CreateGoodsDeliveryCommandHandler(IRepository<AutomotivePart> automotivePartRepository,
            IRepository<GoodsDeliveryNote> goodsDeliveryNoteRepository,
            IRepository<GoodsDeliveryNoteDetail> goodsDeliveryNoteDetailRepository,
            IRepository<AutomotivePartSupplier> automotivePartSupplierRepository,
            IRepository<AutomotivePartInWarehouse> automotivePartInWarehouseRepository)
        {
            _automotivePartRepository = automotivePartRepository;
            _goodsDeliveryNoteRepository = goodsDeliveryNoteRepository;
            _goodsDeliveryNoteDetailRepository = goodsDeliveryNoteDetailRepository;
            _automotivePartSupplierRepository = automotivePartSupplierRepository;
            _automotivePartInWarehouseRepository = automotivePartInWarehouseRepository;
        }

        public async Task<ServiceResult> Handle(CreateGoodsDeliveryCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            request.GoodsDeliveryCode = request.GoodsDeliveryCode.RemoveAllWhiteSpaces();

            var goodsDeliveryNoteCode = await _goodsDeliveryNoteRepository.GetWithIncludeAsync(x => x.GoodsDeliveryCode == request.GoodsDeliveryCode);

            if (goodsDeliveryNoteCode.Any())
            {
                result.IsSuccess = false;
                result.ErrorMessages = new List<string> { "GoodsDeliveryCode already exists" };
                return result;
            }

            var goodsDeliveryNote = new GoodsDeliveryNote
            {
                StaffId = request.StaffId,
                ReceiveDate = request.ReceiveDate,
                GoodsDeliveryCode = request.GoodsDeliveryCode,
            };

            await _goodsDeliveryNoteRepository.AddAsync(goodsDeliveryNote);


            foreach (var x in request.GoodsDeliveryNoteDetails)
            {
                var automotivePart = await _automotivePartRepository.GetByIdAsync(x.AutomotivePartId);

                if (automotivePart == null)
                {
                    throw (new Exception("Not found AutomotivePart by Id"));
                }

                var automotivePartInWarehouses = await _automotivePartInWarehouseRepository.GetWithIncludeAsync(a => a.AutomotivePartId == x.AutomotivePartId);

                var automotivePartInWarehouse = automotivePartInWarehouses.FirstOrDefault();

                if (automotivePartInWarehouse == null)
                {
                    automotivePartInWarehouse = new AutomotivePartInWarehouse
                    {
                        ReceivePrice = x.Price,
                        AutomotivePartId = x.AutomotivePartId,
                        Quantity = x.Quantity,
                    };
                    await _automotivePartInWarehouseRepository.AddAsync(automotivePartInWarehouse);
                }
                else
                {
                    automotivePartInWarehouse.Quantity += x.Quantity;
                    automotivePartInWarehouse.UpdatedOn = DateTime.Now;
                    automotivePartInWarehouse.ReceivePrice = x.Price;

                    await _automotivePartInWarehouseRepository.UpdateAsync(automotivePartInWarehouse);
                }

                var goodsDeliveryNoteDetail = new GoodsDeliveryNoteDetail
                {
                    ReceiveNumber = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    AutomotivePartInWarehouseId = automotivePartInWarehouse.Id,
                    GoodsDeliveryNoteId = goodsDeliveryNote.Id,
                };

                await _goodsDeliveryNoteDetailRepository.AddAsync(goodsDeliveryNoteDetail);
            }

            await _goodsDeliveryNoteDetailRepository.SaveChangeAsync();

            return result;
        }
    }
}
