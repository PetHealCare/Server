using BusinessObjects.Models;
using DTOs.Request.Customer;
using DTOs.Response.Customer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICustomerService
    {
        public List<CustomerResponse> GetAll();
        public Task<Customer> Login(LoginCustomerRequest loginCustomerRequest);
        public Task<bool> Register(RegisterRequest registerRequest);
        public Task<bool> UpdateProfile(UpdateProfileCustomerResquest customerResquest);
        public CustomerResponse GetCustomerById(int customerId);
        public Task<bool> UpdateCustomer(CustomerRequest request);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public List<CustomerResponse> GetAll()
        {
            var customers = _repo.GetAll();
            List<CustomerResponse> customerResponses = new List<CustomerResponse>();
            foreach (var customer in customers)
            {
                CustomerResponse customerResponse = new CustomerResponse();
                customerResponse.CustomerId = customer.CustomerId;
                customerResponse.FullName = customer.FullName;
                customerResponse.PhoneNumber = customer.PhoneNumber;
                customerResponse.Email = customer.Email;
                customerResponse.Password = customer.Password;
                customerResponse.Address = customer.Address;
                customerResponse.Status = customer.Status;
                customerResponses.Add(customerResponse);
            }
            return customerResponses;
        }

        public CustomerResponse GetCustomerById(int customerId)
        {
            var customer = _repo.GetCustomerById(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }
            CustomerResponse customerResponse = new CustomerResponse();
            customerResponse.CustomerId = customer.CustomerId;
            customerResponse.FullName = customer.FullName;
            customerResponse.PhoneNumber = customer.PhoneNumber;
            customerResponse.Email = customer.Email;
            customerResponse.Password = customer.Password;
            customerResponse.Address = customer.Address;
            customerResponse.Status = customer.Status;
            return customerResponse;
        }

        public async Task<Customer> Login(LoginCustomerRequest loginCustomerRequest)
        {
            return await _repo.Login(loginCustomerRequest);
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
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
            return await _repo.Register(customer);
        }

        public async Task<bool> UpdateCustomer(CustomerRequest request)
        {
            var customer = new Customer();
            customer.CustomerId = request.CustomerId;
            customer.FullName = request.FullName;
            customer.PhoneNumber = request.PhoneNumber;
            customer.Email = request.Email;
            customer.Password = request.Password;
            customer.Address = request.Address;
            customer.Status = request.Status;

            return await _repo.UpdateCustomer(customer);
        }

        public async Task<bool> UpdateProfile(UpdateProfileCustomerResquest customerResquest)
        {
           
           return await _repo.UpdateProfile(customerResquest);
        }
    }
}
