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
        public string? Note { get; set; }

        public List<int> ServiceIds { get; set; }

        public string? RoomNo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? SlotBooking { get; set; }
        public bool? Status { get; set; }
    }
}
