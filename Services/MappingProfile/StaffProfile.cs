using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Staff;
using DTOs.Request.User;
using DTOs.Response.Staff;
using DTOs.Response.Service;

namespace Services.MappingProfile
{
	public class StaffProfile : Profile
	{
        public StaffProfile()
        {
			CreateMap<CreateStaffRequest, staff>();
			CreateMap<CreateUserRequest, User>();
			CreateMap<UpdateStaffRequest, staff>();
			CreateMap<staff, StaffResponse>();
		}
    }
}
