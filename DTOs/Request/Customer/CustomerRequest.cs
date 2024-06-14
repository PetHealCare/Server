using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Customer
{
    public class CustomerRequest
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool? Status { get; set; }
        public int UserId { get; set; }
    }
}
