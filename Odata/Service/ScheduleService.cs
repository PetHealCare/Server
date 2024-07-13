using AutoMapper;
using DTOs.Response.Schedule;
using Repositories;
using Repositories.Interface;

namespace Odata.Service
{
	public interface IScheduleService
	{
		Task<ScheduleResponse> GetById(int id);
		Task<List<ScheduleResponse>> GetList();
	}

	public class ScheduleService : IScheduleService
	{
		private readonly IScheduleRepository _repo;
		private readonly IMapper _mapper;

		public ScheduleService(IScheduleRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<ScheduleResponse> GetById(int id)
		{
			var pet = await _repo.Get(id);
			if (pet.Status == false)
			{
				return null;
			}
			var response = _mapper.Map<ScheduleResponse>(pet);
			return response;
		}

		public async Task<List<ScheduleResponse>> GetList()
		{
			var petlist = await _repo.GetAll();

			return _mapper.Map<List<ScheduleResponse>>(petlist);
		}
	}
}
