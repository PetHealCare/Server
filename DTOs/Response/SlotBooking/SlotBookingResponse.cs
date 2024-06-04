using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.SlotBooking
{
    public class SlotBookingResponse
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int ServiceId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhoneNumber { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorSpeciality { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int? Price { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }
    }
}
