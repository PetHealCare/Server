using BusinessObjects.Models;
using DTOs.Request.Customer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IStaffService
    {
        public List<staff> GetAll();
        public staff Get(int id);
        public Task<bool> Update(staff request);
        public Task<staff> Create(staff request);
        public bool Delete(int id);
        public Task<staff> Login(LoginCustomerRequest loginCustomerRequest);

    }
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<staff> Create(staff request)
        {
           return  _staffRepository.Create(request);
        }

        public bool Delete(int id)
        {
            return _staffRepository.Delete(id);
        }

        public staff Get(int id)
        {
            return _staffRepository.Get(id);
        }

        public List<staff> GetAll()
        {
            return _staffRepository.GetAll();
        }

        public async Task<staff> Login(LoginCustomerRequest loginCustomerRequest)
        {
            return  _staffRepository.Login(loginCustomerRequest.Email, loginCustomerRequest.Password);
        }

        public async Task<bool> Update(staff request)
        {
            return _staffRepository.Update(request);
        }
    }
}
