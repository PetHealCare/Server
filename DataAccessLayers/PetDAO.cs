using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessLayers
{
	public class PetDAO : GenericDAO<Pet>
	{
		private static readonly Lazy<PetDAO> _instance =
		new Lazy<PetDAO>(() => new PetDAO(new PetHealthCareContext()));
		public static PetDAO Instance => _instance.Value;
		public PetDAO(PetHealthCareContext context) : base(context)
		{

		}
	}
}
