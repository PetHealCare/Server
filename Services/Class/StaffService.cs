using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DataAccessLayers;
using DTOs.Request.Staff;
using DTOs.Response.Staff;
using DTOs.Response.Service;
using DTOs;
using Repositories.Interface;
using Repositories;
using Services.Extentions.Paginate;
using Services.Interface;
using Presentation.Client;

namespace Services.Class
{
	public class StaffService : IStaffService
	{
		private readonly IStaffRepository _repo;
		private readonly IUserRepository _userRepo;
		private readonly IMapper _mapper;
		private readonly OdataClient _odataClient;

		public StaffService(IStaffRepository repo, IUserRepository userRepo, IMapper mapper, OdataClient odataClient)
		{
			_repo = repo;
			_userRepo = userRepo;
			_mapper = mapper;
			_odataClient = odataClient;
		}

		public async Task<StaffResponse> Create(CreateStaffRequest request)
		{
			var user = _mapper.Map<User>(request);
			user.Role = (int)RoleEnum.Staff;
			var userCreated = await _userRepo.Create(user);
			var staff = _mapper.Map<staff>(request);
			staff.UserId = userCreated.UserId;
			var staffCreated = _repo.Create(staff);
			var response = _mapper.Map<StaffResponse>(staff);
			return response;
		}

		public async Task<bool> Delete(int id)
		{
			return _repo.Delete(id);
		}

		public async Task<StaffResponse> GetById(int id)
		{
			var staff = await _odataClient.GetStaffByIdAsync(id);
			if(staff == null)
			{
				return null;
			}
			var staffResponse = _mapper.Map<StaffResponse>(staff);
			return staffResponse;
		}

		public async Task<StaffResponse> GetByUserId(int userId)
		{
			var staff = _repo.Get(userId);
			if (staff == null)
			{
				return null;
			}
			var staffResponse = _mapper.Map<StaffResponse>(staff);
			return staffResponse;
		}

		public async Task<PaginatedList<StaffResponse>> GetList(GetListStaffRequest request)
		{
			var response = new PaginatedList<StaffResponse>();
			var staffsQuery = ( await _odataClient.GetStaffAsync()).AsQueryable();


			if (!string.IsNullOrEmpty(request.FullName))
			{
				staffsQuery = staffsQuery.Where(p => p.FullName.Contains(request.FullName));
			}

			var filterredStaffs = staffsQuery.ToList();

			return await filterredStaffs.ToPaginateAsync(request);
		}

		public async Task<StaffResponse> Update(UpdateStaffRequest request)
		{
			var staff = _repo.Get(request.StaffId);
			if (staff == null)
			{
				return null; // or throw an exception, based on your error handling strategy
			}

			if (!string.IsNullOrEmpty(request.Password))
			{
				var user = await _userRepo.GetUserById(staff.UserId);
				user.Password = request.Password;
				UserDAO.Instance.Update(user);
			}

			// Update the fields
			staff.FullName = request.FullName ?? staff.FullName;
			staff.PhoneNumber = request.PhoneNumber ?? staff.PhoneNumber;

			// Save changes
			_repo.Update(staff);

			// Map updated staff to response
			var response = _mapper.Map<StaffResponse>(staff);
			return response;
		}
	}
}
