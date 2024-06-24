using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.Request.User;

namespace DTOs.Request.Staff
{
	public class CreateStaffRequest : CreateUserRequest
	{
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
	}
}
