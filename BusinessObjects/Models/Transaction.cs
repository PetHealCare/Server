using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int BillId { get; set; }
        public int PaymentId { get; set; }
        public double? Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
    }
}
