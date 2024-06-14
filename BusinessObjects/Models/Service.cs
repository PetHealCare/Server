using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Service
    {
        public Service()
        {
            Bookings = new HashSet<Booking>();
            Doctors = new HashSet<Doctor>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public int? LimitTime { get; set; }
        public int? Price { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
