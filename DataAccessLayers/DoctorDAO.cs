using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayers
{
	public class DoctorDAO: GenericDAO<Doctor>
	{
		private static readonly Lazy<DoctorDAO> _instance =
		new Lazy<DoctorDAO>(() => new DoctorDAO(new PetHealthCareContext()));
		public static DoctorDAO Instance => _instance.Value;
		public DoctorDAO(PetHealthCareContext context) : base(context)
		{

		}

		public Doctor GetDoctorByUserId(int id)
		{
			return _context.Doctors
				.Include(d => d.Services)
				.FirstOrDefault(d => d.UserId == id);
		}
	}
}
