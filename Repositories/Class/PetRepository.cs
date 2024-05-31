using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessLayers;
using DTOs;
using DTOs.Request;
using Repositories.Interface;

namespace Repositories.Class
{
	public class PetRepository : IPetRepository
	{
		public async Task<Pet> Create(Pet request) => PetDAO.Instance.Create(request);

		public async Task<IList<Pet>> GetList() => PetDAO.Instance.GetAll();

		public async Task<Pet> GetPetById(int id) => PetDAO.Instance.GetById(id);

		public async Task<Pet> Update(Pet request) => PetDAO.Instance.UpdateNew(request);
	}
}
