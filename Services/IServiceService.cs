using BusinessObjects.Models;
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
        public List<Service> GetAll();
        public Service Get(int id);
        public Service Create(Service service);
        public bool Update(Service service);
        public bool Delete(int id);
        
    }
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;
        public ServiceService(IServiceRepository repo)
        {
            _repo = repo;
        }

        public Service Create(Service service)
        {
            return _repo.Create(service);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public Service Get(int id)
        {
            return _repo.GetById(id);
        }

        public List<Service> GetAll()
        {
            return _repo.GetAll();
        }

        public bool Update(Service service)
        {
            return _repo.Update(service);
        }
    }
}
