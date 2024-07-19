using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessLayers;
using Repositories.Interface;

namespace Repositories.Class
{
	public class TransactionRepository : ITransactionRepository
	{
		public async Task<Transaction> Create(Transaction request) => TransactionDAO.Instance.Create(request);

		public async Task<IList<Transaction>> GetList() => TransactionDAO.Instance.GetAll();

		public async Task<Transaction> GetTransactionById(int id) => TransactionDAO.Instance.GetById(id);
	}
}
