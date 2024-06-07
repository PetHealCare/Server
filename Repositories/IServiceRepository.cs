using BusinessObjects.Models;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IServiceRepository
    {
        public List<Service> GetAll();
        public Service GetById(int id);
        public Service Create(Service service);
        public Task<bool> Update(Service service);
        public bool Delete(int id);

    }
    public class ServiceRepository : IServiceRepository
    {
        public Service Create(Service service)
        {
            return ServiceDAO.Instance.Create(service);
        }

        public bool Delete(int id)
        {
            return ServiceDAO.Instance.Delete(id);
        }

        public List<Service> GetAll()
        {
            return ServiceDAO.Instance.GetAll();
        }

        public Service GetById(int id)
        {
            return ServiceDAO.Instance.GetById(id);
        }

        public async Task<bool> Update(Service service)
        {
            return await ServiceDAO.Instance.UpdateService(service);
        }
    }
}
