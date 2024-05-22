using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            Pets = new HashSet<Pet>();
        }

        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
