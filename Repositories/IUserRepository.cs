using BusinessObjects.Models;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        public List<User> GetAll();
    }
    public class UserRepository : IUserRepository
    {
        public  List<User> GetAll()
        {
            return  UserDAO.Instance.GetAll();
        }
    }
}
