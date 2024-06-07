using DTOs.Response.SlotBooking;
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
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string PetName { get; set; }
        public string PetSpecies { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Note { get; set; }
        public bool? Status { get; set; }
        public List<SlotBookingResponse> SlotBookings { get; set; }
    }
}
