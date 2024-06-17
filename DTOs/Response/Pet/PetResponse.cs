using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Pet
{
    public class PetResponse
    {
		public int PetId { get; set; }
		public string? Name { get; set; }
		public string? Species { get; set; }
		public bool? Status { get; set; }
		public int CustomerId { get; set; }
		public int? Age { get; set; }
		public bool? Gender { get; set; }
		public string? Generic { get; set; }
		public string? Description { get; set; }
	}
}
