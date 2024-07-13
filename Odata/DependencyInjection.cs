﻿using Repositories.Class;
using Repositories.Interface;
using Repositories;
using Odata.Service;
using Services.MappingProfile;

namespace Odata
{
	public static class DependencyInjection
	{
		public static void AddPackage(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile).Assembly);
			services.AddCors(options =>
			{
				options.AddPolicy("AllowReactApp",
					builder =>
					{
						builder.WithOrigins("http://localhost:3000", "https://localhost:7083", "http://localhost:7083")
							   .AllowAnyHeader()
							   .AllowAnyMethod()
							   .AllowCredentials();
					});
			});

		}
		public static void AddMasterServices(this IServiceCollection services)
		{

			services.AddScoped<IPetService, PetService>();
			services.AddScoped<IPetRepository, PetRepository>();

			services.AddScoped<IDoctorService, DoctorService>();
			services.AddScoped<IDoctorRepository, DoctorRepository>();

			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddScoped<IScheduleRepository, ScheduleRepository>();
		}
	}
}
