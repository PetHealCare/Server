using AutoMapper;
using DTOs.Response.Staff;
using Repositories;
using Repositories.Interface;

namespace Odata.Service
{
	public interface IStaffService
	{
		Task<StaffResponse> GetById(int id);
		Task<List<StaffResponse>> GetList();
	}

	public class StaffService : IStaffService
	{
		private readonly IStaffRepository _repo;
		private readonly IMapper _mapper;

		public StaffService(IStaffRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<StaffResponse> GetById(int id)
		{
			var staff = _repo.Get(id);

			var response = _mapper.Map<StaffResponse>(staff);
			return response;
		}

		public async Task<List<StaffResponse>> GetList()
		{
			var stafflist =  _repo.GetAll();

			return _mapper.Map<List<StaffResponse>>(stafflist);
		}
	}
}
