using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Bill
{
    public class BillResponse
    {
        public int BillId { get; set; }
        public int BookingId { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime? InsDate { get; set; }
    }
}
