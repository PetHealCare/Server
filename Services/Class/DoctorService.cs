using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Doctor;
using DTOs.Response.Doctor;
using Repositories.Interface;
using Services.Extentions.Paginate;
using Services.Interface;

namespace Services.Class
{
	public class DoctorService : IDoctorService
	{
		private readonly IDoctorRepository _repo;
		private readonly IMapper _mapper;
		public DoctorService(IDoctorRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<DoctorResponse> Create(CreateDoctorRequest request)
		{
			var doctor = _mapper.Map<Doctor>(request);
			var doctorCreated = await _repo.Create(doctor);
			var response = _mapper.Map<DoctorResponse>(doctor);
			return response;
		}

		public async Task<bool> Delete(int id)
		{
			var doctorToDelete = await _repo.GetDoctorById(id);
			if (doctorToDelete == null)
			{
				return false;
			}
			doctorToDelete.Status = false;
			await _repo.Update(doctorToDelete);
			return true;
		}

		public async Task<DoctorResponse> GetById(int id)
		{
			var doctor = await _repo.GetDoctorById(id);
			if (doctor.Status == false)
			{
				return null;
			}
			var response = _mapper.Map<DoctorResponse>(doctor);
			return response;
		}

		public async Task<PaginatedList<DoctorResponse>> GetList(GetListDoctorRequest request)
		{
			var response = new PaginatedList<DoctorResponse>();
			var doctorsQuery = (await _repo.GetList()).AsQueryable();

			//filter doctor has not been deleted
			doctorsQuery = doctorsQuery.Where(p => p.Status == true);

			if (!string.IsNullOrEmpty(request.FullName))
			{
				doctorsQuery = doctorsQuery.Where(p => p.FullName.Contains(request.FullName));
			}

			var filterredDoctors = doctorsQuery.ToList();
			var mapperList = _mapper.Map<IList<DoctorResponse>>(filterredDoctors);
			response = await mapperList.ToPaginateAsync(request);
			return response;
		}

		public async Task<DoctorResponse> Update(UpdateDoctorRequest request)
		{
			var doctor = await _repo.GetDoctorById(request.DoctorId);
			if (doctor == null || doctor.Status == false)
			{
				return null; // or throw an exception, based on your error handling strategy
			}

			// Update the fields
			doctor.FullName = request.FullName ?? doctor.FullName;
			doctor.PhoneNumber = request.PhoneNumber ?? doctor.PhoneNumber;
			doctor.Email = request.Email ?? doctor.Email;
			doctor.Speciality = request.Speciality ?? doctor.Speciality;

			// Save changes
			await _repo.Update(doctor);

			// Map updated doctor to response
			var response = _mapper.Map<DoctorResponse>(doctor);
			return response;
		}
	}
}
