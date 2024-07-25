using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DataAccessLayers;
using DTOs;
using DTOs.Request.Customer;
using DTOs.Request.Doctor;
using DTOs.Response.Doctor;
using DTOs.Response.Service;
using Presentation.Client;
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
        private readonly IServiceRepository _serviceRepo;
        private readonly IMapper _mapper;
		private readonly OdataClient _odataClient;

        public DoctorService(IDoctorRepository repo, IUserRepository userRepo, IServiceRepository serviceRepo, IMapper mapper, OdataClient odataClient)
        {
            _repo = repo;
            _userRepo = userRepo;
            _serviceRepo = serviceRepo;
            _mapper = mapper;
            _odataClient = odataClient;
        }

        public async Task<DoctorResponse> AddService(AddServiceRequest request)
        {
            var response = new DoctorResponse();
			var doctor = await _repo.GetDoctorById(request.DoctorId);
			if (doctor == null || doctor.Status == false)
			{
                return null;
            }
			foreach (var serviceId in request.ListServiceIds)
			{
                var service = _serviceRepo.GetById(serviceId);
                if (service == null)
				{
                    return null;
                }
                doctor.Services.Add(service);
            }
			return response;
        }

        public async Task<DoctorResponse> Create(CreateDoctorRequest request)
		{
			var user = _mapper.Map<User>(request);
			user.Role = (int)RoleEnum.Doctor;
			user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
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
			var doctor = await _odataClient.GetDoctorByIdAsync(id);
			if (doctor.Status == false)
			{
				return null;
			}
			return doctor;
		}

		public async Task<DoctorResponse> GetByUserId(int userId)
		{
			var doctor = await _repo.GetDoctorByUserId(userId);
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
			var doctorsQuery = (await _odataClient.GetDoctorAsync()).AsQueryable();

			//filter doctor has not been deleted
			doctorsQuery = doctorsQuery.Where(p => p.Status == true);

			if (!string.IsNullOrEmpty(request.FullName))
			{
				doctorsQuery = doctorsQuery.Where(p => p.FullName.Contains(request.FullName));
			}

			var filterredDoctors = doctorsQuery.ToList();

			return await filterredDoctors.ToPaginateAsync(request);
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
				user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
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
