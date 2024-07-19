using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.Transaction
{
	public class TransactionResponse
	{
		public int TransactionId { get; set; }
		public int BillId { get; set; }
		public int PaymentId { get; set; }
		public double? Amount { get; set; }
		public DateTime TransactionDate { get; set; }
	}
}
