using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            MedicalRecords = new HashSet<MedicalRecord>();
            SlotBookings = new HashSet<SlotBooking>();
        }

        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Speciality { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<SlotBooking> SlotBookings { get; set; }
    }
}
