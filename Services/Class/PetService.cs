using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request;
using DTOs.Request.Pet;
using DTOs.Response.Pet;
using Repositories.Interface;
using Services.Extentions.Paginate;
using Services.Interface;

namespace Services.Class
{
    public class PetService : IPetService
	{
		private readonly IPetRepository _repo;
		private readonly IMapper _mapper;
		public PetService(IPetRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<PetResponse> Create(CreatePetRequest request)
		{
			var pet = _mapper.Map<Pet>(request); ;

			var petCreated = await _repo.Create(pet);
			var response = _mapper.Map<PetResponse>(pet);
			return response;
		}

		public async Task<bool> Delete(int id)
		{
			var petToDelete = await _repo.GetPetById(id);
			if (petToDelete == null)
			{
				return false;
			}
			petToDelete.Status = false;
			await _repo.Update(petToDelete);
			return true;
		}

		public async Task<PetResponse> GetById(int id)
		{
			var pet = await _repo.GetPetById(id);
			if (pet.Status == false)
			{
				return null;
			}
			var response = _mapper.Map<PetResponse>(pet);
			return response;
		}

		public async Task<PaginatedList<PetResponse>> GetList(GetListPetRequest request)
		{
			var response = new PaginatedList<PetResponse>();
			var petsQuery = (await _repo.GetList()).AsQueryable();

			//filter pet has not been deleted
			petsQuery = petsQuery.Where(p => p.Status == true);
			
			if (!string.IsNullOrEmpty(request.Name))
			{
				petsQuery = petsQuery.Where(p => p.Name.Contains(request.Name));
			}

			if (!string.IsNullOrEmpty(request.Species))
			{
				petsQuery = petsQuery.Where(p => p.Species.Contains(request.Species));
			}

			if(request.CustomerId != null)
			{
				petsQuery = petsQuery.Where(p => p.CustomerId == request.CustomerId);
			}
			var filterredPets = petsQuery.ToList();
			var mapperList = _mapper.Map<IList<PetResponse>>(filterredPets);
			response = await mapperList.ToPaginateAsync(request);
			return response;
		}

		public async Task<PetResponse> Update(UpdatePetRequest request)
		{
			var pet = await _repo.GetPetById(request.PetId);
			if (pet == null || pet.Status == false)
			{
				return null; // or throw an exception, based on your error handling strategy
			}

			// Update the fields
			pet.Name = request.Name ?? pet.Name;
			pet.Species = request.Species ?? pet.Species;
			pet.Description = request.Description ?? pet.Description;
			pet.Generic = request.Generic ?? pet.Generic;
			pet.Age = request.Age ?? pet.Age;

			// Save changes
			await _repo.Update(pet);

			// Map updated pet to response
			var response = _mapper.Map<PetResponse>(pet);
			return response;
		}

	}
}
