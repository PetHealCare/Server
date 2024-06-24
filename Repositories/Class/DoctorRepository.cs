using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessLayers;
using Repositories.Interface;

namespace Repositories.Class
{
	public class DoctorRepository : IDoctorRepository
	{
		public async Task<Doctor> Create(Doctor request) => DoctorDAO.Instance.Create(request);

		public async Task<IList<Doctor>> GetList(string includeProperties = "") => DoctorDAO.Instance.GetAll(includeProperties);

		public async Task<Doctor> GetDoctorById(int id, string includeProperties = "") => DoctorDAO.Instance.GetById(id, includeProperties);

		public async Task<Doctor> Update(Doctor request) => DoctorDAO.Instance.UpdateNew(request);
	}
}
