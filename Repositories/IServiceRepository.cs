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

    }
    public class ServiceRepository : IServiceRepository
    {
        public List<Service> GetAll()
        {
            return ServiceDAO.Instance.GetAll();
        }
    }
}
