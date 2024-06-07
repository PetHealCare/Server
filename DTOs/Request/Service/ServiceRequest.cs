using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Service
{
    public class ServiceRequest
    {
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
    }
}
