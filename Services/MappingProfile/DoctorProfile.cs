using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Doctor;
using DTOs.Request.User;
using DTOs.Response.Doctor;
using DTOs.Response.Service;

namespace Services.MappingProfile
{
	public class DoctorProfile : Profile
	{
        public DoctorProfile()
        {
			CreateMap<CreateDoctorRequest, Doctor>();
			CreateMap<CreateUserRequest, User>();
			CreateMap<UpdateDoctorRequest, Doctor>();
			CreateMap<BusinessObjects.Models.Service, ServiceResponse>();
			CreateMap<Doctor, DoctorResponse>();
			//.ForMember(dest => dest.ServiceList, opt => opt.MapFrom(src => src.Services));
		}
	}
}
