using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs.Request.Pet;
using DTOs.Response;
using Services.Extentions.Paginate;

namespace Services.Interface
{
    public interface IPetService
	{
		public Task<PetResponse> Create(CreatePetRequest request);
		public Task<PetResponse> Update(UpdatePetRequest request);
		public Task<PetResponse> GetById(int id);
		public Task<PaginatedList<PetResponse>> GetList(GetListPetRequest request);
	}
}
