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
		public Task<User> Create(User request);
		public Task<User> Update(User request);
		public Task<User> GetUserById(int id);
		public Task<IList<User>> GetList();
	}
    public class UserRepository : IUserRepository
    {
        public async Task<IList<User>> GetList() => UserDAO.Instance.GetAll();

		public async Task<User> Create(User request) => UserDAO.Instance.Create(request);

		public async Task<User> GetUserById(int id) => UserDAO.Instance.GetById(id);

		public async Task<User> Update(User request) => UserDAO.Instance.UpdateNew(request);
	}
}
