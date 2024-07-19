using AutoMapper;
using BusinessObjects.Models;
using DTOs.Response.Booking;
using DTOs.Response.Customer;
using DTOs.Response.Doctor;
using DTOs.Response.Pet;
using DTOs.Response.Schedule;
using DTOs.Response.Service;
using Repositories;
using Repositories.Interface;

namespace Odata.Service
{
	public interface IBookingService
	{
		Task<BookingResponse> GetById(int id);
		Task<List<BookingResponse>> GetList();
	}

	public class BookingService : IBookingService
	{
		private readonly IBookingRepository _repo;
		private readonly IMapper _mapper;

		public BookingService(IBookingRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<BookingResponse> GetById(int id)
		{
			var item = await _repo.GetBookingById(id);
			if (item == null)
			{
				throw new Exception("Booking not found");
			}
			var booking = new BookingResponse();
			booking.BookingId = item.BookingId;
			booking.PetId = item.PetId;
			booking.CustomerId = item.CustomerId;
			booking.DoctorId = item.DoctorId;
			booking.ScheduleId = item.ScheduleId;
			booking.Slot = item.Slot;
			booking.BookingDate = item.BookingDate.Value;
			booking.Note = item.Note;
			booking.Status = item.Status;
			booking.Pet = new PetResponse
			{
				PetId = item.Pet.PetId,
				Name = item.Pet.Name,
				Species = item.Pet.Species,
				Status = item.Pet.Status,
				CustomerId = item.Pet.CustomerId,
				Age = item.Pet.Age,
				Gender = item.Pet.Gender,
				Generic = item.Pet.Generic,
				Description = item.Pet.Description
			};
			booking.Customer = new CustomerResponse
			{
				CustomerId = item.Customer.CustomerId,
				FullName = item.Customer.FullName,
				PhoneNumber = item.Customer.PhoneNumber,
				Address = item.Customer.Address,
				Status = item.Customer.Status,
				UserId = item.Customer.UserId
			};
			booking.Doctor = new DoctorResponse
			{
				DoctorId = item.Doctor.DoctorId,
				FullName = item.Doctor.FullName,
				PhoneNumber = item.Doctor.PhoneNumber,
				Speciality = item.Doctor.Speciality,
				Status = item.Doctor.Status,
				UserId = item.Doctor.UserId
			};
			booking.Schedule = new ScheduleResponse
			{
				ScheduleId = item.Schedule.ScheduleId,
				DoctorId = item.Schedule.DoctorId,
				RoomNo = item.Schedule.RoomNo,
				StartTime = item.Schedule.StartTime,
				EndTime = item.Schedule.EndTime,
				SlotBooking = item.Schedule.SlotBooking,
				Status = item.Schedule.Status
			};
			booking.Services = new List<ServiceResponse>();
			foreach (var service in item.Services)
			{
				booking.Services.Add(new ServiceResponse
				{
					ServiceId = service.ServiceId,
					ServiceName = service.ServiceName,
					Description = service.Description,
					Price = service.Price,
					LimitTime = service.LimitTime,

				});
			}


			return booking;
		}

		public async Task<List<BookingResponse>> GetList()
		{
			var bookings = await _repo.GetAll();
			var response = new List<BookingResponse>();
			foreach (var item in bookings)
			{
				var booking = new BookingResponse();
				booking.BookingId = item.BookingId;
				booking.PetId = item.PetId;
				booking.CustomerId = item.CustomerId;
				booking.DoctorId = item.DoctorId;
				booking.ScheduleId = item.ScheduleId;
				booking.Slot = item.Slot;
				booking.BookingDate = item.BookingDate.Value;
				booking.Note = item.Note;
				booking.Status = item.Status;
				booking.Pet = new PetResponse
				{
					PetId = item.Pet.PetId,
					Name = item.Pet.Name,
					Species = item.Pet.Species,
					Status = item.Pet.Status,
					CustomerId = item.Pet.CustomerId,
					Age = item.Pet.Age,
					Gender = item.Pet.Gender,
					Generic = item.Pet.Generic,
					Description = item.Pet.Description
				};
				booking.Customer = new CustomerResponse
				{
					CustomerId = item.Customer.CustomerId,
					FullName = item.Customer.FullName,
					PhoneNumber = item.Customer.PhoneNumber,
					Address = item.Customer.Address,
					Status = item.Customer.Status,
					UserId = item.Customer.UserId
				};
				booking.Doctor = new DoctorResponse
				{
					DoctorId = item.Doctor.DoctorId,
					FullName = item.Doctor.FullName,
					PhoneNumber = item.Doctor.PhoneNumber,
					Speciality = item.Doctor.Speciality,
					Status = item.Doctor.Status,
					UserId = item.Doctor.UserId
				};
				booking.Schedule = new ScheduleResponse
				{
					ScheduleId = item.Schedule.ScheduleId,
					DoctorId = item.Schedule.DoctorId,
					RoomNo = item.Schedule.RoomNo,
					StartTime = item.Schedule.StartTime,
					EndTime = item.Schedule.EndTime,
					SlotBooking = item.Schedule.SlotBooking,
					Status = item.Schedule.Status
				};
				booking.Services = new List<ServiceResponse>();
				foreach (var service in item.Services)
				{
					booking.Services.Add(new ServiceResponse
					{
						ServiceId = service.ServiceId,
						ServiceName = service.ServiceName,
						Description = service.Description,
						Price = service.Price,
						LimitTime = service.LimitTime,

					});
				}
				response.Add(booking);
			}
			return response;
		}
	}
}
