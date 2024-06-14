using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Schedule
{
    public class ScheduleResponse
    {
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public string? RoomNo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? SlotBooking { get; set; }
        public bool? Status { get; set; }
    }
}
