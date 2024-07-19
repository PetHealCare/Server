using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request.Paging;

namespace DTOs.Request.Transaction
{
	public class GetListTransactionRequest : PagingRequest
	{
		public double? AmountFrom { get; set; }
		public double? AmountTo { get; set; }
	}
}
