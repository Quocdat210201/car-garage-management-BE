using Gara.Domain.ServiceResults;
using Gara.Management.Domain.Entities;
using Gara.Persistance.Abstractions;
using MediatR;

namespace Gara.Management.Domain.Queries.GoodsDeliverys
{
    public class GetGoodsDeliveryNoteListQuery : IRequest<ServiceResult>
    {
    }

    public class GetGoodsDeliveryNoteListQueryHandler : IRequestHandler<GetGoodsDeliveryNoteListQuery, ServiceResult>
    {
        private readonly IRepository<GoodsDeliveryNote> _goodsDeliveryNoteRepository;
        private readonly IRepository<AutomotivePartInWarehouse> _automotivePartInWarehouse;

        public GetGoodsDeliveryNoteListQueryHandler(IRepository<GoodsDeliveryNote> goodsDeliveryNoteRepository, IRepository<AutomotivePartInWarehouse> automotivePartRepository)
        {
            _goodsDeliveryNoteRepository = goodsDeliveryNoteRepository;
            _automotivePartInWarehouse = automotivePartRepository;
        }

        public async Task<ServiceResult> Handle(GetGoodsDeliveryNoteListQuery request, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();

            var goodsDeliveryNotes = await _goodsDeliveryNoteRepository.GetWithIncludeAsync(null, 0, 0, x => x.Staff, y => y.GoodsDeliveryNoteDetails);

            foreach (var goodsDeliveryNote in goodsDeliveryNotes)
            {
                foreach (var goodsDeliveryNoteDetail in goodsDeliveryNote.GoodsDeliveryNoteDetails)
                {
                    var automotivePart = await _automotivePartInWarehouse.GetWithIncludeAsync(x => x.Id == goodsDeliveryNoteDetail.AutomotivePartInWarehouseId, 0, 0, x => x.AutomotivePart, x => x.AutomotivePart.AutomotivePartSupplier);

                    goodsDeliveryNoteDetail.AutomotivePartInWarehouse = automotivePart.FirstOrDefault();
                }
            }

            result.Success(goodsDeliveryNotes);

            return result;
        }
    }
}
