using BusinessObjects.Models;
using DTOs;
using DTOs.Request.Customer;
using DTOs.Response.Customer;
using Presentation.Client;
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
        public Task<List<CustomerResponse>> GetAll();
        public Task<User> Login(LoginRequest loginCustomerRequest);
        public Task<bool> Register(RegisterRequest registerRequest);
        public Task<bool> UpdateProfile(UpdateProfileCustomerResquest customerResquest);
        public Task<CustomerResponse> GetCustomerById(int customerId);
        public Task<bool> UpdateCustomer(CustomerRequest request);
        public CustomerResponse GetCustomerByUserId(int userId);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IUserRepository _userRepository;
        private readonly OdataClient _odataClient;
        public CustomerService(ICustomerRepository repo, IUserRepository userRepository, OdataClient _odataClient)
        {
            _repo = repo;
            _userRepository = userRepository;
            this._odataClient = _odataClient;
        }
        public async Task<List<CustomerResponse>> GetAll()
        {
            var customers = await _odataClient.GetCustomersAsync();
            return customers;
        }

        public async Task<CustomerResponse> GetCustomerById(int customerId)
        {
            var customer = await _odataClient.GetCustomerByIdAsync( customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }
            return customer;
        }

        public CustomerResponse GetCustomerByUserId(int userId)
        {
           var customer =  _repo.GetCustomerByUserId(userId);
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
            if ((await _userRepository.GetList()).Any(x => x.Email.Equals(registerRequest.Email)))
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
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
            user.Role = (int)RoleEnum.Customer;
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
