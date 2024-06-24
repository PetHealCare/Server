using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Staff
{
	public class StaffResponse
	{
		public int StaffId { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }
		public int UserId { get; set; }
	}
}
