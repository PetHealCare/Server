using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Repositories.Interface
{
	public interface ITransactionRepository
	{
			public Task<Transaction> Create(Transaction request);
			public Task<Transaction> GetTransactionById(int id);
			public Task<IList<Transaction>> GetList();
	}
}
