using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Booking
{
    public class SlotBookingRequest
    {
        public int SlotBookingId { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public int ScheduleId { get; set; }
        public bool? Status { get; set; }
    }
}
