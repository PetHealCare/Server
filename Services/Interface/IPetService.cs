using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs.Request;
using DTOs.Response;

namespace Services.Interface
{
	public interface IPetService
	{
		public Task<PetResponse> Create(CreatePetRequest request);
		public Task<PetResponse> Update(UpdatePetRequest request);
		public Task<PetResponse> GetById(int id);
		public Task<List<PetResponse>> GetList(GetListPetRequest request);
	}
}
