using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Bookings = new HashSet<Booking>();
            MedicalRecords = new HashSet<MedicalRecord>();
        }

        public int PetId { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public bool? Status { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
