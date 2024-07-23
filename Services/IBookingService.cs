using AutoMapper;
using BusinessObjects.Models;
using DataAccessLayers;
using DTOs.Request.Booking;
using DTOs.Request.Schedule;
using DTOs.Response.Booking;
using DTOs.Response.Customer;
using DTOs.Response.Doctor;
using DTOs.Response.Pet;
using DTOs.Response.Schedule;
using DTOs.Response.Service;
using Microsoft.Extensions.FileProviders;
using Presentation.Client;
using Repositories;
using Services.Extentions.Paginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public interface IBookingService
	{
		public Task<List<BookingResponse>> GetBookings(GetListBookingRequest request);
		public Task<Booking> CreateBooking(BookingRequest booking);
		public Task<bool> UpdateStatusBooking(int bookingId);
		public Task<BookingResponse> GetBookingById(int booking);
		public Task<bool> UpdateBooking(BookingRequest request);
		public Task<bool> DeleteBooking(int bookingId);
		public Task<bool> CreateBookingWithService(BookingServiceRequest request);
	}
	public class BookingService : IBookingService
	{
		private readonly IBookingRepository _repo;
		private readonly IMapper _mapper;
		private readonly IScheduleRepository _scheduleRepository;
		private readonly IServiceRepository _serviceRepository;
		private readonly OdataClient _odataClient;

		public BookingService(IBookingRepository repo, IMapper mapper, IScheduleRepository scheduleRepository, IServiceRepository serviceRepository, OdataClient _odataClient)
		{
			_repo = repo;
			_mapper = mapper;
			_scheduleRepository = scheduleRepository;
			_serviceRepository = serviceRepository;
			this._odataClient = _odataClient;
		}

		public async Task<Booking> CreateBooking(BookingRequest request)
		{
			Booking booking = new Booking();
			booking.PetId = request.PetId;
			booking.CustomerId = request.CustomerId;
			booking.DoctorId = request.DoctorId;
			booking.ScheduleId = request.ScheduleId;
			booking.BookingDate = request.BookingDate;
			booking.Slot = request.Slot;
			booking.Note = request.Note;
			booking.Status = request.Status;

			return await _repo.CreateBooking(booking);
		}

        public async Task<bool> CreateBookingWithService(BookingServiceRequest request)
		{
            var existingSchedules = await _scheduleRepository.GetSchedulesByDoctorIdAndRoomNo(request.DoctorId, request.RoomNo);
            foreach (var existingSchedule in existingSchedules)
            {
                if (existingSchedule.StartTime.HasValue && existingSchedule.EndTime.HasValue)
                {
                    if (existingSchedule.StartTime.Value.Date == request.StartTime.Value.Date &&
                        (request.StartTime < existingSchedule.EndTime.Value && request.EndTime > existingSchedule.StartTime.Value))
                    {
                        throw new ArgumentException("The requested time slot overlaps with an existing schedule on the same day and room.");
                    }
                }
            }
            var schedule = new Schedule();
			schedule.DoctorId = request.DoctorId;
			schedule.RoomNo = request.RoomNo;
			schedule.StartTime = request.StartTime;
			schedule.EndTime = request.EndTime;
			schedule.SlotBooking = request.SlotBooking;
			schedule.Status = true;
			var createSchedule = _scheduleRepository.Create(schedule);
			if (request.ServiceIds.Count > 5)
			{
				throw new ArgumentException("You can only add up to 5 services per booking.");
			}
			var booking = new Booking();
			booking.PetId = request.PetId;
			booking.CustomerId = request.CustomerId;
			booking.DoctorId = request.DoctorId;
			booking.ScheduleId = createSchedule.Result.ScheduleId;
			booking.BookingDate = DateTime.Now;
			booking.Slot = createSchedule.Result.SlotBooking;
			booking.Note = request.Note;
			booking.Status = true;
			var result = await _repo.CreateBooking(booking);
			if (result == null)
			{
				return false;
			}
			foreach (var serviceId in request.ServiceIds)
			{
				var service = _serviceRepository.GetById(serviceId);
				if (service != null)
				{
					result.Services.Add(service);
				}
				await _repo.Update(result);

			}

			var status = await _scheduleRepository.updateStatus(schedule.ScheduleId);
			if (status == false)
			{
				return false;
			}
			return true;
		}



		public Task<bool> DeleteBooking(int bookingId)
		{
			return _repo.DeleteBooking(bookingId);
		}

		public async Task<BookingResponse> GetBookingById(int bookingid)
		{
			return await _odataClient.GetBookingByIdAsync(bookingid);
		}

		public async Task<List<BookingResponse>> GetBookings(GetListBookingRequest request)
		{
			var bookingsQuerry = (await _odataClient.GetBookingsAsync()).AsQueryable();
			if (request.PetId != 0)
			{
				bookingsQuerry = bookingsQuerry.Where(b => b.PetId == request.PetId);
			}
			if (request.CustomerId != 0)
			{
				bookingsQuerry = bookingsQuerry.Where(b => b.CustomerId == request.CustomerId);
			}
			if (request.DoctorId != 0)
			{
				bookingsQuerry = bookingsQuerry.Where(b => b.DoctorId == request.DoctorId);
			}
			if (request.ScheduleId != 0)
			{
				bookingsQuerry = bookingsQuerry.Where(b => b.ScheduleId == request.ScheduleId);
			}
			if (!string.IsNullOrEmpty(request.Status.ToString()))
			{
				bookingsQuerry = bookingsQuerry.Where(b => b.Status == request.Status);
			}

			return bookingsQuerry.ToList();
		}

		public Task<bool> UpdateBooking(BookingRequest request)
		{
			var booking = _mapper.Map<Booking>(request);
			return _repo.Update(booking);
		}

		public async Task<bool> UpdateStatusBooking(int bookingId)
		{
			return await _repo.UpdateStatusBooking(bookingId);
		}


	}
}
