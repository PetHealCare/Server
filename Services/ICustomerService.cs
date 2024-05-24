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
        public Customer Login(LoginCustomerRequest loginCustomerRequest);
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

        public Customer Login(LoginCustomerRequest loginCustomerRequest)
        {
            return _repo.Login(loginCustomerRequest);
        }
    }
}
