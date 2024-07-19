using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Bills = new HashSet<Bill>();
            Transactions = new HashSet<Transaction>();
        }

        public int PaymentId { get; set; }
        public double? Amount { get; set; }
        public string? Method { get; set; }
        public DateTime? InsDate { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
