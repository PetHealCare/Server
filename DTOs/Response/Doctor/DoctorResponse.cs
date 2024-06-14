using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Doctor
{
	public class DoctorResponse
	{
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Speciality { get; set; }
        public bool? Status { get; set; }
        public int UserId { get; set; }
    }
}
