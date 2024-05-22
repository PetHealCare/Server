using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Booking
    {
        public Booking()
        {
            SlotBookings = new HashSet<SlotBooking>();
        }

        public int BookingId { get; set; }
        public int PetId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Note { get; set; }
        public bool? Status { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Pet Pet { get; set; } = null!;
        public virtual Bill? Bill { get; set; }

        public virtual ICollection<SlotBooking> SlotBookings { get; set; }
    }
}
