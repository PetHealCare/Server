using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class BookingRequest
    {
        public int PetId { get; set; }
        public int CustomerId { get; set; }
       
        public string? Note { get; set; }
       
    }
}
