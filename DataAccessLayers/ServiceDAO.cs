using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class ServiceDAO : GenericDAO<Service>
    {
        private static readonly Lazy<ServiceDAO> _instance =
        new Lazy<ServiceDAO>(() => new ServiceDAO(new PetHealthCareContext()));
        public static ServiceDAO Instance => _instance.Value;
        public ServiceDAO(PetHealthCareContext context) : base(context)
        {

        }

        public async Task<bool> UpdateService(Service service)
        {
            var serviceToUpdate = GetById(service.ServiceId);
            if (serviceToUpdate == null)
            {
                return false;
            }
            serviceToUpdate.ServiceName = service.ServiceName;
            serviceToUpdate.Description = service.Description;
            serviceToUpdate.Price = service.Price;
            _context.Services.Update(serviceToUpdate);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
