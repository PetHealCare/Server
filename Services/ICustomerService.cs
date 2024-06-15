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
        public Task<User> Login(LoginRequest loginCustomerRequest);
        public Task<bool> Register(RegisterRequest registerRequest);
        public Task<bool> UpdateProfile(UpdateProfileCustomerResquest customerResquest);
        public CustomerResponse GetCustomerById(int customerId);
        public Task<bool> UpdateCustomer(CustomerRequest request);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IUserRepository _userRepository;
        public CustomerService(ICustomerRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
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
                customerResponse.Address = customer.Address;
                customerResponse.Status = customer.Status;
                customerResponse.UserId = customer.UserId;
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
            customerResponse.Address = customer.Address;
            customerResponse.Status = customer.Status;
            customerResponse.UserId = customer.UserId;
            return customerResponse;
        }

        public async Task<User> Login(LoginRequest loginRequest)
        {
            return await _repo.Login(loginRequest);
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Email) || string.IsNullOrWhiteSpace(registerRequest.Password))
            {
                throw new ArgumentException("Email and password are required.");
            }
            if (_userRepository.GetAll().Any(x => x.Email.Equals(registerRequest.Email)))
            {
                throw new Exception("A user with this email already exists.");
            }
            Customer customer = new Customer();
            customer.FullName = registerRequest.FullName;
            customer.PhoneNumber = registerRequest.PhoneNumber;
            customer.Address = registerRequest.Address;
            customer.Status = true;
            
            User user = new User();
            user.Email = registerRequest.Email;
            user.Password = registerRequest.Password;
            user.Role = 3;
            var userResponse = await _repo.RegisterUser(user);
            if(userResponse == null)
            {
                throw new Exception("Register failed.");
            }
            customer.UserId = userResponse.UserId;
            return await _repo.Register(customer);
        }

        public async Task<bool> UpdateCustomer(CustomerRequest request)
        {
            var customer = new Customer();
            customer.CustomerId = request.CustomerId;
            customer.FullName = request.FullName;
            customer.PhoneNumber = request.PhoneNumber;
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
