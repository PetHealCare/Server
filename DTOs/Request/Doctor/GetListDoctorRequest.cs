using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request.Paging;

namespace DTOs.Request.Doctor
{
	public class GetListDoctorRequest : PagingRequest
	{
		public string? FullName { get; set; }
	}
}
