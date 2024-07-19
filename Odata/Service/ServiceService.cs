using AutoMapper;
using DTOs.Response.Service;
using DTOs.Response.Service;
using Repositories;
using Repositories.Interface;

namespace Odata.Service
{
	public interface IServiceService
	{
		Task<ServiceResponse> GetById(int id);
		Task<List<ServiceResponse>> GetList();
	}

	public class ServiceService : IServiceService
	{
		private readonly IServiceRepository _repo;
		private readonly IMapper _mapper;

		public ServiceService(IServiceRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<ServiceResponse> GetById(int id)
		{
			var service = _repo.GetById(id);
			if (service == null)
			{
				throw new Exception("Service not found.");
			}
			var serviceResponse = new ServiceResponse();
			serviceResponse.ServiceId = service.ServiceId;
			serviceResponse.ServiceName = service.ServiceName;
			serviceResponse.Description = service.Description;
			serviceResponse.LimitTime = service.LimitTime;
			serviceResponse.Price = service.Price;
			return serviceResponse;
		}

		public async Task<List<ServiceResponse>> GetList()
		{
			var services = _repo.GetAll();
			List<ServiceResponse> serviceResponses = new List<ServiceResponse>();
			foreach (var service in services)
			{
				ServiceResponse serviceResponse = new ServiceResponse();
				serviceResponse.ServiceId = service.ServiceId;
				serviceResponse.ServiceName = service.ServiceName;
				serviceResponse.Description = service.Description;
				serviceResponse.LimitTime = service.LimitTime;
				serviceResponse.Price = service.Price;
				serviceResponses.Add(serviceResponse);
			}
			return serviceResponses;
		}
	}
}
