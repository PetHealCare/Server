using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class SlotBooking
    {
        public SlotBooking()
        {
            Bookings = new HashSet<Booking>();
        }

        public int SlotBookingId { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public int ScheduleId { get; set; }
        public bool? Status { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
