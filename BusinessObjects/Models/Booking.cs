using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Bills = new HashSet<Bill>();
            Services = new HashSet<Service>();
        }

        public int BookingId { get; set; }
        public int PetId { get; set; }
        public int CustomerId { get; set; }
        public int DoctorId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime? BookingDate { get; set; }
        public byte? Slot { get; set; }
        public string? Note { get; set; }
        public bool? Status { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Pet Pet { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
