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
    }
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;
        public ServiceService(IServiceRepository repo)
        {
            _repo = repo;
        }

        public List<Service> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
