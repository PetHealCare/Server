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
    }
}
