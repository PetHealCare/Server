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

		public async Task<IList<Doctor>> GetList() => DoctorDAO.Instance.GetAll();

		public async Task<Doctor> GetDoctorById(int id) => DoctorDAO.Instance.GetById(id);

		public async Task<Doctor> Update(Doctor request) => DoctorDAO.Instance.UpdateNew(request);
	}
}
