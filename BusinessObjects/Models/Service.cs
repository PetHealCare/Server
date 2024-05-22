using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Service
    {
        public Service()
        {
            SlotBookings = new HashSet<SlotBooking>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }

        public virtual ICollection<SlotBooking> SlotBookings { get; set; }
    }
}
