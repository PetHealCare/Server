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

        public bool Resgiter(RegisterRequest registerRequest)
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Email) || string.IsNullOrWhiteSpace(registerRequest.Password))
            {
                throw new ArgumentException("Email and password are required.");
            }
            if (GetAll().Any(x => x.Email.Equals(registerRequest.Email)))
            {
                throw new Exception("A user with this email already exists.");
            }

            Customer customer = new Customer();
            customer.FullName = registerRequest.FullName;
            customer.PhoneNumber = registerRequest.PhoneNumber;
            customer.Email = registerRequest.Email;
            customer.Password = registerRequest.Password;
            customer.Address = registerRequest.Address;
            customer.Status = true;
            _context.Add(customer);
            return _context.SaveChanges() > 0;
           
        }
    }
}
