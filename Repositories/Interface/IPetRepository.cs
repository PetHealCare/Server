using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs;
using DTOs.Request;

namespace Repositories.Interface
{
	public interface IPetRepository
	{
		public Task<Pet> Create(Pet request);
		public Task<Pet> Update(Pet request);
		public Task<Pet> GetPetById(int id);
		public Task<IList<Pet>> GetList();

	}
}
