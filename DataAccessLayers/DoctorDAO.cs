using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

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
	}
}
