using AutoMapper;
using DTOs.Response.Transaction;
using DTOs.Response.Service;
using Repositories.Interface;

namespace Odata.Service
{
	public interface ITransactionService
	{
		Task<TransactionResponse> GetById(int id);
		Task<List<TransactionResponse>> GetList();
	}

	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _repo;
		private readonly IMapper _mapper;

		public TransactionService(ITransactionRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<TransactionResponse> GetById(int id)
		{
			var transaction = await _repo.GetTransactionById(id);
			var transactionResponse = _mapper.Map<TransactionResponse>(transaction);
			return transactionResponse;
		}

		public async Task<List<TransactionResponse>> GetList()
		{
			var transactionlist = await _repo.GetList();
			var transactionResponses = _mapper.Map<List<TransactionResponse>>(transactionlist);
			return transactionResponses.ToList();
		}
	}
}
