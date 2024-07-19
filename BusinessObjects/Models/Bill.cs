using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Bill
    {
        public Bill()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int BillId { get; set; }
        public int BookingId { get; set; }
        public double? TotalAmount { get; set; }
        public int PaymentId { get; set; }
        public DateTime? InsDate { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
