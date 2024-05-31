using BusinessObjects.Models;
using DataAccessLayers;
using Repositories;
using Repositories.Class;
using Repositories.Interface;
using Services;
using Services.Class;
using Services.Interface;
using Services.MappingProfile;

namespace Presentation
{
    /// <summary>
    /// Functions for create dependency injections
    /// </summary>
    public static class DependencyInjection
	{
		/// <summary>
		/// This function to add dependency injection for NuGet Package
		/// </summary>
		/// <param name="services"></param>
		public static void AddPackage(this IServiceCollection services)
		{
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") // Update with your React app URL
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });
            //Add other service in nuget package
            services.AddSwaggerGen();
			services.AddAutoMapper(typeof(MappingProfile).Assembly);
		}

		/// <summary>
		/// Create dependencies for service (interface) & service (class) or repository (interface) & repository (class)
		/// </summary>
		/// <param name="services"></param>
		public static void AddMasterServices(this IServiceCollection services)
		{
			// Add dependency injection for class and interface
			//EX:
			//services.AddScoped<IAccountRepository, AccountRepository>();
			//services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IServiceRepository, ServiceRepository>();
			services.AddScoped<IServiceService, ServiceService>();
			services.AddScoped<ISlotBookingRepository, SlotBookingRepository>();
			services.AddScoped<ISlotBookingService, SlotBookingService>();
			services.AddScoped<IBookingRepository, BookingRepository>();
			services.AddScoped<IBookingService, BookingService>();
			services.AddScoped<IScheduleRepository, ScheduleRepository>();
			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddScoped<IPetRepository, PetRepository>();
			services.AddScoped<IPetService, PetService>();
		}
	}

}
