using BusinessObjects.Models;
using DTOs.Request.Service;
using DTOs.Response.Service;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IServiceService
    {
        public List<ServiceResponse> GetAll();
        public ServiceResponse Get(int id);
        public Service Create(ServiceRequest service);
        public Task<bool> Update(ServiceRequest service);
        public bool Delete(int id);
        
    }
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;
        public ServiceService(IServiceRepository repo)
        {
            _repo = repo;
        }

        public  Service Create(ServiceRequest request)
        {
            var service = new Service();
            service.ServiceName = request.ServiceName;
            service.Description = request.Description;
            service.Price = request.Price;


            return  _repo.Create(service);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public ServiceResponse Get(int id)
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
            serviceResponse.Price = service.Price;
            return serviceResponse;
        }

        public List<ServiceResponse> GetAll()
        {
            var services = _repo.GetAll();
            List<ServiceResponse> serviceResponses = new List<ServiceResponse>();
            foreach (var service in services)
            {
                ServiceResponse serviceResponse = new ServiceResponse();
                serviceResponse.ServiceId = service.ServiceId;
                serviceResponse.ServiceName = service.ServiceName;
                serviceResponse.Description = service.Description;
                serviceResponse.Price = service.Price;
                serviceResponses.Add(serviceResponse);
            }
            return serviceResponses;
        }

        public async Task<bool> Update(ServiceRequest request)
        {
            var service = new Service();
            service.ServiceId = request.ServiceId;
            service.ServiceName = request.ServiceName;
            service.Description = request.Description;
            service.Price = request.Price;
            return await _repo.Update(service);
        }
    }
}
