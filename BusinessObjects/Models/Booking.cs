using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Bills = new HashSet<Bill>();
        }

        public int BookingId { get; set; }
        public int PetId { get; set; }
        public int CustomerId { get; set; }
        public int SlotBooking { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Note { get; set; }
        public bool? Status { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Pet Pet { get; set; } = null!;
        public virtual SlotBooking SlotBookingNavigation { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
