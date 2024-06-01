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
        public int CustomerId { get; set; }
    }
}
