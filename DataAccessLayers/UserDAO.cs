using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class UserDAO : GenericDAO<User>
    {
        private static readonly Lazy<UserDAO> _instance =
        new Lazy<UserDAO>(() => new UserDAO(new PetHealthCareContext()));
        public static UserDAO Instance => _instance.Value;
        public UserDAO(PetHealthCareContext context) : base(context)
        {

        }
        public User Register(User request)
        {
            _context.Add(request);
            _context.SaveChanges();
            return request;
        }
    }
}
