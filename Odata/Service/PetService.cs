using AutoMapper;
using DTOs.Request.Pet;
using DTOs.Response.Pet;
using Repositories.Interface;

namespace Odata.Service
{

	public interface IPetService
	{
		Task<PetResponse> GetById(int id);
		Task<List<PetResponse>> GetList();
	}

	public class PetService : IPetService
	{
		private readonly IPetRepository _repo;
		private readonly IMapper _mapper;

		public PetService(IPetRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
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

		public async Task<List<PetResponse>> GetList()
		{
			var petlist = await _repo.GetList();

			return _mapper.Map<List<PetResponse>>(petlist);
		}
	}
}
