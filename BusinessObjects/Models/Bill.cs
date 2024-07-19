using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Bill
    {
        public Bill()
        {
            Payments = new HashSet<Payment>();
            Transactions = new HashSet<Transaction>();
        }

        public int BillId { get; set; }
        public int BookingId { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime? InsDate { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
