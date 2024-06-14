using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class staff
    {
        public int StaffId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
