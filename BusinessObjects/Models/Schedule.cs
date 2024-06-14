using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public string? RoomNo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? SlotBooking { get; set; }
        public bool? Status { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
