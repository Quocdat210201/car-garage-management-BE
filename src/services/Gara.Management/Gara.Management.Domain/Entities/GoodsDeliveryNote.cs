﻿using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class GoodsDeliveryNote : EntityBaseWithId
    {
        public string GoodsDeliveryCode { get; set; }

        public Guid StaffId { get; set; }

        public DateTime ReceiveDate { get; set; }

        public GaraApplicationUser Staff { get; set; }

        public List<GoodsDeliveryNoteDetail> GoodsDeliveryNoteDetails { get; set; }
    }
}
