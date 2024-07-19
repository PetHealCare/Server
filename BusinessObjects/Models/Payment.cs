using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public double? Amount { get; set; }
        public string? Method { get; set; }
        public DateTime? InsDate { get; set; }
        public string? Status { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual Transaction? Transaction { get; set; }
    }
}
