using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessLayers
{
	public class CustomerDAO : GenericDAO<Customer>
	{
		private static readonly Lazy<CustomerDAO> _instance =
		new Lazy<CustomerDAO>(() => new CustomerDAO(new PetHealthCareContext()));
		public static CustomerDAO Instance => _instance.Value;
		public CustomerDAO(PetHealthCareContext context) : base(context)
		{
		}
	}
}
