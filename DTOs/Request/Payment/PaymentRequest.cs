using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.Payment
{
    public class PaymentRequest
    {
        public int PaymentId { get; set; }
        public double? Amount { get; set; }
        public string? Method { get; set; }
        public string? Status { get; set; }
        public int BillId { get; set; }
    }
}
