using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Payment
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public double? Amount { get; set; }
        public string? Method { get; set; }
        public DateTime? InsDate { get; set; }
        public string? Status { get; set; }
        public int BillId { get; set; }
    }
}
