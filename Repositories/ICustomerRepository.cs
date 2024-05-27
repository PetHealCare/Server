using BusinessObjects.Models;
using DataAccessLayers;
using DTOs;
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
        public Customer Login(LoginCustomerRequest loginCustomerRequest);
        public bool Register(RegisterRequest registerRequest);

    }
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAll()
        {
            return CustomerDAO.Instance.GetAll();
        }

        public Customer Login(LoginCustomerRequest loginCustomerRequest)
        {
            return CustomerDAO.Instance.Login(loginCustomerRequest);
        }

        public bool Register(RegisterRequest registerRequest)
        {
            return CustomerDAO.Instance.Resgiter(registerRequest);
        }
    }
}
