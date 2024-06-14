using DTOs.Response.Customer;
using DTOs.Response.Doctor;
using DTOs.Response.Pet;
using DTOs.Response.Schedule;
using DTOs.Response.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Booking
{
    public class BookingResponse
    {
        public int BookingId { get; set; }
        public int PetId { get; set; }
        public int CustomerId { get; set; }
        public int DoctorId { get; set; }
        public int ScheduleId { get; set; }
        public byte? Slot { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Note { get; set; }
        public bool? Status { get; set; }
        public PetResponse Pet { get; set; }
        public DoctorResponse Doctor { get; set; }
        public CustomerResponse Customer { get; set; }
        public ScheduleResponse Schedule { get; set; }
        public List<ServiceResponse> Services { get; set; }

        
    }
}
