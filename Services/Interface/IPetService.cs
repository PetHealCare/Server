using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DTOs.Request.Pet;
using DTOs.Response.Pet;
using Services.Extentions.Paginate;

namespace Services.Interface
{
    public interface IPetService
	{
		Task<PetResponse> Create(CreatePetRequest request);
		Task<PetResponse> Update(UpdatePetRequest request);
		Task<PetResponse> GetById(int id);
		Task<PaginatedList<PetResponse>> GetList(GetListPetRequest request);
		Task<bool> Delete(int id);
	}
}
