using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Service;
using DTOs.Response.Service;
using Presentation.Client;
using Repositories;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IServiceService
    {
        public Task<List<ServiceResponse>> GetAll();
        public Task<ServiceResponse> Get(int id);
        public Task<ServiceResponse> Create(ServiceRequest service);
        public Task<bool> Update(ServiceRequest service);
        public bool Delete(int id);
        
    }
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IMapper _mapper;
        private readonly OdataClient _odataClient;

		public ServiceService(IServiceRepository repo, IDoctorRepository doctorRepo, IMapper mapper, OdataClient _client)
		{
			_repo = repo;
			_doctorRepo = doctorRepo;
			_mapper = mapper;
            this._odataClient = _client;
		}

		public async Task<ServiceResponse> Create(ServiceRequest request)
        {
            var service = new Service();
            service.ServiceName = request.ServiceName;
            service.Description = request.Description;
            service.LimitTime = request.LimitTime;
            service.Price = request.Price;
            service = _repo.Create(service);

            return  _mapper.Map<ServiceResponse>(service);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public async Task<ServiceResponse> Get(int id)
        { 
            var service = await _odataClient.GetServiceByIdAsync(id);
            if (service == null)
            {
                throw new Exception("Service not found.");
            }
            return service;
        }

        public async  Task<List<ServiceResponse>> GetAll()
        {
            var services = await _odataClient.GetServicesAsync();
			return services;
        }

        public async Task<bool> Update(ServiceRequest request)
        {
            var service = new Service();
            service.ServiceId = request.ServiceId;
            service.ServiceName = request.ServiceName;
            service.Description = request.Description;
            service.LimitTime = request.LimitTime;
            service.Price = request.Price;
            return await _repo.Update(service);
        }
    }
}
