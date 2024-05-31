using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request
{
	public class CreatePetRequest
	{
		public string? Name { get; set; }
		public string? Species { get; set; }
		public int CustomerId { get; set; }
	}
}
