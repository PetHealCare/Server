using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Doctors = new HashSet<Doctor>();
            staff = new HashSet<staff>();
        }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
