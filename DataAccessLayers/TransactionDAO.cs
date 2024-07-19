using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayers
{
	public class TransactionDAO : GenericDAO<Transaction>
	{
		private static readonly Lazy<TransactionDAO> _instance =
		new Lazy<TransactionDAO>(() => new TransactionDAO(new PetHealthCareContext()));
		public static TransactionDAO Instance => _instance.Value;
		public TransactionDAO(PetHealthCareContext context) : base(context)
		{

		}
	}
}
