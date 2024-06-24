using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Staff
{
	public class UpdateStaffRequest
	{
		public int StaffId { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
