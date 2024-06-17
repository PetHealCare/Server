using BusinessObjects.Models;
using DataAccessLayers;
using DTOs.Request.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICustomerRepository
    {
        public List<Customer> GetAll();
        public Task<User> Login(LoginRequest loginCustomerRequest);
        public Task<bool> Register(Customer customer);
        public Task<User> RegisterUser(User user);
        public Task<bool> UpdateProfile(UpdateProfileCustomerResquest customerResquest);
        public Customer GetCustomerById(int customerId);
        public Task<bool> UpdateCustomer(Customer customer);
        public Customer GetCustomerByUserId(int id);

    }
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAll()
        {
            return CustomerDAO.Instance.GetAll();
        }

        public Customer GetCustomerById(int customerId)
        {
            return CustomerDAO.Instance.GetById(customerId);
        }

        public Customer GetCustomerByUserId(int id)
        {
            return CustomerDAO.Instance.GetCustomerByUserId(id);
        }

        public async Task<User> Login(LoginRequest loginCustomerRequest)
        {
            return CustomerDAO.Instance.Login(loginCustomerRequest);
        }

        public async Task<bool> Register(Customer customer)
        {
            return CustomerDAO.Instance.Resgiter(customer);
        }

        public async Task<User> RegisterUser(User user)
        {
            return  UserDAO.Instance.Register(user);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            return  CustomerDAO.Instance.UpdateCustomer(customer);
        }

        public async Task<bool> UpdateProfile(UpdateProfileCustomerResquest updateProfileCustomerResquest)
        {
            return CustomerDAO.Instance.UpdateProfile(updateProfileCustomerResquest);
        }
    }
}
