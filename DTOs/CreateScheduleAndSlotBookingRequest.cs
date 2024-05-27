using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CreateScheduleAndSlotBookingRequest
    {
        public int bookingId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        
    }
}
