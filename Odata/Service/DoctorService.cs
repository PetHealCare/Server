using AutoMapper;
using DTOs.Response.Doctor;
using DTOs.Response.Service;
using Repositories.Interface;

namespace Odata.Service
{
	public interface IDoctorService
	{
		Task<DoctorResponse> GetById(int id);
		Task<List<DoctorResponse>> GetList();
	}

	public class DoctorService : IDoctorService
	{
		private readonly IDoctorRepository _repo;
		private readonly IMapper _mapper;

		public DoctorService(IDoctorRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<DoctorResponse> GetById(int id)
		{
			var doctor = await _repo.GetDoctorById(id, includeProperties: "Services");
			if (doctor.Status == false)
			{
				return null;
			}
			var doctorResponse = _mapper.Map<DoctorResponse>(doctor);
			doctorResponse.ServiceList = doctor.Services.Select(doctorService =>
			{
				var serviceResponse = _mapper.Map<ServiceResponse>(doctorService);
				return serviceResponse;
			}).ToList();
			return doctorResponse;
		}

		public async Task<List<DoctorResponse>> GetList()
		{
			var doctorlist = await _repo.GetList(includeProperties: "Services");
			var doctorResponses = doctorlist.Select(doctor =>
			{
				var doctorResponse = _mapper.Map<DoctorResponse>(doctor);

				// Map DoctorDetails to DoctorDetailResponse and include Product information
				doctorResponse.ServiceList = doctor.Services.Select(doctorService =>
				{
					var serviceResponse = _mapper.Map<ServiceResponse>(doctorService);
					return serviceResponse;
				}).ToList();

				return doctorResponse;
			});
			return doctorResponses.ToList();
		}
	}
}
