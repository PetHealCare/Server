using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.Request.User;

namespace DTOs.Request.Doctor
{
	public class CreateDoctorRequest : CreateUserRequest
	{
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string Speciality { get; set; }
	}
}
