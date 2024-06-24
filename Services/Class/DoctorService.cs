using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DataAccessLayers;
using DTOs;
using DTOs.Request.Doctor;
using DTOs.Response.Doctor;
using DTOs.Response.Service;
using Repositories;
using Repositories.Interface;
using Services.Extentions.Paginate;
using Services.Interface;

namespace Services.Class
{
	public class DoctorService : IDoctorService
	{
		private readonly IDoctorRepository _repo;
		private readonly IUserRepository _userRepo;
		private readonly IMapper _mapper;

		public DoctorService(IDoctorRepository repo, IUserRepository userRepo, IMapper mapper)
		{
			_repo = repo;
			_userRepo = userRepo;
			_mapper = mapper;
		}

		public async Task<DoctorResponse> Create(CreateDoctorRequest request)
		{
			var user = _mapper.Map<User>(request);
			user.Role = (int)RoleEnum.Doctor;
			var userCreated = await _userRepo.Create(user);
			var doctor = _mapper.Map<Doctor>(request);
			doctor.UserId = userCreated.UserId;
			doctor.Status = true;
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

		public async Task<PaginatedList<DoctorResponse>> GetList(GetListDoctorRequest request)
		{
			var response = new PaginatedList<DoctorResponse>();
			var doctorsQuery = (await _repo.GetList(includeProperties: "Services")).AsQueryable();

			//filter doctor has not been deleted
			doctorsQuery = doctorsQuery.Where(p => p.Status == true);

			if (!string.IsNullOrEmpty(request.FullName))
			{
				doctorsQuery = doctorsQuery.Where(p => p.FullName.Contains(request.FullName));
			}

			var filterredDoctors = doctorsQuery.ToList();

			var doctorResponses = filterredDoctors.Select(doctor =>
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

			return await doctorResponses.ToPaginateAsync(request);
		}

		public async Task<DoctorResponse> Update(UpdateDoctorRequest request)
		{
			var doctor = await _repo.GetDoctorById(request.DoctorId);
			if (doctor == null || doctor.Status == false)
			{
				return null; // or throw an exception, based on your error handling strategy
			}

			if (!string.IsNullOrEmpty(request.Password))
			{
				var user = await _userRepo.GetUserById(doctor.UserId);
				user.Password = request.Password;
				UserDAO.Instance.Update(user);
			}

			// Update the fields
			doctor.FullName = request.FullName ?? doctor.FullName;
			doctor.PhoneNumber = request.PhoneNumber ?? doctor.PhoneNumber;
			doctor.Speciality = request.Speciality ?? doctor.Speciality;

			// Save changes
			await _repo.Update(doctor);

			// Map updated doctor to response
			var response = _mapper.Map<DoctorResponse>(doctor);
			return response;
		}
	}
}
