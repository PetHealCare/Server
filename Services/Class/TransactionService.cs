using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTOs.Request.Transaction;
using DTOs.Response.Pet;
using DTOs.Response.Transaction;
using Presentation.Client;
using Repositories.Interface;
using Services.Extentions.Paginate;
using Services.Interface;

namespace Services.Class
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _repo;
		private readonly IMapper _mapper;
		private readonly OdataClient _odataClient;

		public TransactionService(ITransactionRepository repo, IMapper mapper, OdataClient odataClient)
		{
			_repo = repo;
			_mapper = mapper;
			_odataClient = odataClient;
		}
		public async Task<PaginatedList<TransactionResponse>> GetList(GetListTransactionRequest request)
		{
			var response = new PaginatedList<TransactionResponse>();
			var transactionsQuery = (await _odataClient.GetTransactionsAsync()).AsQueryable();

			// Apply filters based on AmountFrom and AmountTo
			if (request.AmountFrom.HasValue)
			{
				transactionsQuery = transactionsQuery.Where(t => t.Amount >= request.AmountFrom.Value);
			}

			if (request.AmountTo.HasValue)
			{
				transactionsQuery = transactionsQuery.Where(t => t.Amount <= request.AmountTo.Value);
			}

			var filterredTransactions = transactionsQuery.ToList();
			response = await filterredTransactions.ToPaginateAsync(request);
			return response;
		}

		public async Task<TransactionResponse> GetById(int id)
		{
			return await _odataClient.GetTransactionByIdAsync(id);
		}
	}
}
