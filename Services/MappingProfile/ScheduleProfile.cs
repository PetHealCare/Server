using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Schedule;
using DTOs.Request.User;
using DTOs.Response.Schedule;

namespace Services.MappingProfile
{
	public class ScheduleProfile : Profile
	{
		public ScheduleProfile()
		{
			CreateMap<Schedule, ScheduleResponse>();
		}
	}
}
