﻿using Gara.Domain;

namespace Gara.Management.Domain.Entities
{
    public class AppointmentSchedule : EntityBaseWithId
    {
        public DateTime AppointmentDate { get; set; }

        public string Content { get; set; }

        public int Status { get; set; }

        public int ReceiveCarAt { get; set; }

        public string ReceiveCarAddress { get; set; }

        public Guid CarId { get; set; }

        public Car Car { get; set; }
    }
}