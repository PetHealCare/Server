using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Schedule
{
    public class GetListScheduleRequest
    {
       
        public int DoctorId { get; set; }
        public string? RoomNo { get; set; }
        public bool? Status { get; set; }
    }
}
