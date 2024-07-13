using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual staff? staff { get; set; }
    }
}
