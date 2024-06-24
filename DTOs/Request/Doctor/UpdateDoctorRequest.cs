using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Doctor
{
	public class UpdateDoctorRequest
	{
		public int DoctorId { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public string Speciality { get; set; }
	}
}
