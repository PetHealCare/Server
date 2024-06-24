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

namespace Services.MappingProfile
{
	public class DoctorProfile : Profile
	{
        public DoctorProfile()
        {
			CreateMap<CreateDoctorRequest, Doctor>();
			CreateMap<CreateUserRequest, User>();
			CreateMap<UpdateDoctorRequest, Doctor>();
			CreateMap<Doctor, DoctorResponse>();
		}
    }
}
