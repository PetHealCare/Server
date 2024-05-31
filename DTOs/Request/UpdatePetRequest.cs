﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs.Request
{
	public class UpdatePetRequest
	{
		public int PetId { get; set; }
		public string? Name { get; set; }
		public string? Species { get; set; }
		public bool? Status { get; set; }
		public int CustomerId { get; set; }
	}
}
