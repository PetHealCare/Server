using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs.Request.Pet
{
	public class UpdatePetRequest
	{
		public int PetId { get; set; }
		public string? Name { get; set; }
		public string? Species { get; set; }
		public int? Age { get; set; }
		public string? Generic { get; set; }
		public string? Description { get; set; }
	}
}