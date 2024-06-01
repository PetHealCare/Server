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
        public int ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Note { get; set; }
        public bool? Status { get; set; }
    }
}
