using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Booking
{
    public class SlotBookingRequest
    {
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public int ScheduleId { get; set; }
    }
}
