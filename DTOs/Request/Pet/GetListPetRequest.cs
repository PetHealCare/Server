using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request.Paging;

namespace DTOs.Request.Pet
{
    public class GetListPetRequest : PagingRequest
    {
		public string? Name { get; set; }
		public string? Species { get; set; }
		public int? CustomerId { get; set; }
	}
}
