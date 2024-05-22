using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            SlotBookings = new HashSet<SlotBooking>();
        }

        public int ScheduleId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<SlotBooking> SlotBookings { get; set; }
    }
}
