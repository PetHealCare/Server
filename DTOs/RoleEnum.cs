using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
	public enum RoleEnum
	{
		[Description("Staff")]
		Staff = 1,

		[Description("Doctor")]
		Doctor = 2,

		[Description("Customer")]
		Customer = 3,
	}
}
