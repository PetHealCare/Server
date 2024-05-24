using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;

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
        public Customer Login(LoginCustomerRequest loginCustomerRequest)
        {
            // Use LINQ to find the customer with the matching email and password
            return _context.Customers
                           .FirstOrDefault(c => c.Email.Equals(loginCustomerRequest.Email) && c.Password.Equals(loginCustomerRequest.Password) && c.Status == true);
        }
    }
}
