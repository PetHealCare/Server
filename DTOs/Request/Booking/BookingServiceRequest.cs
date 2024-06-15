using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Booking
{
    public class BookingServiceRequest
    {
        public int BookingId { get; set; }
        public int PetId { get; set; }
        public int CustomerId { get; set; }
        public int DoctorId { get; set; }
        public int ScheduleId { get; set; }
        public byte? Slot { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Note { get; set; }
        public bool? Status { get; set; }
        public List<int> ServiceIds { get; set; }
    }
}
