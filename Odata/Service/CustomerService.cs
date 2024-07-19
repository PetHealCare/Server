using AutoMapper;
using BusinessObjects.Models;
using DTOs.Response.Customer;
using DTOs.Response.Service;
using Repositories;
using Repositories.Interface;

namespace Odata.Service
{
	public interface ICustomerService
	{
		Task<CustomerResponse> GetById(int id);
		Task<List<CustomerResponse>> GetList();
	}

	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _repo;
		private readonly IMapper _mapper;

		public CustomerService(ICustomerRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<CustomerResponse> GetById(int id)
		{
			var customer = _repo.GetCustomerById(id);
			if (customer == null)
			{
				return null;
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

		public async Task<List<CustomerResponse>> GetList()
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
	}
}
