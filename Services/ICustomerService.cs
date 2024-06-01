using BusinessObjects.Models;
using DTOs;
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
        public List<Customer> GetAll();
        public Task<Customer> Login(LoginCustomerRequest loginCustomerRequest);
        public Task<bool> Register(RegisterRequest registerRequest);
        public Task<bool> UpdateProfile(UpdateProfileCustomerResquest customerResquest);
        public Customer GetCustomerById(int customerId);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public List<Customer> GetAll()
        {
            return _repo.GetAll();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _repo.GetCustomerById(customerId);
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

        public async Task<bool> UpdateProfile(UpdateProfileCustomerResquest customerResquest)
        {
           
           return await _repo.UpdateProfile(customerResquest);
        }
    }
}
