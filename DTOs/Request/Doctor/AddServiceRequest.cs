using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Doctor
{
    public class AddServiceRequest
    {
        public int DoctorId { get; set; }
        public List<int> ListServiceIds { get; set; }
    }
}