using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.Request.Pet;
using DTOs.Request.Transaction;
using DTOs.Response.Pet;
using DTOs.Response.Transaction;
using Services.Extentions.Paginate;

namespace Services.Interface
{
	public interface ITransactionService
	{
		Task<PaginatedList<TransactionResponse>> GetList(GetListTransactionRequest request);
		Task<TransactionResponse> GetById(int id);
	}
}
