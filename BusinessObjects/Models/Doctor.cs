using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Bookings = new HashSet<Booking>();
            MedicalRecords = new HashSet<MedicalRecord>();
            Schedules = new HashSet<Schedule>();
            Services = new HashSet<Service>();
        }

        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Speciality { get; set; }
        public bool? Status { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
