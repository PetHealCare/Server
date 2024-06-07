using AutoMapper;
using BusinessObjects.Models;
using DTOs.Request.Booking;
using DTOs.Request.Schedule;
using DTOs.Response.Booking;
using DTOs.Response.Pet;
using DTOs.Response.SlotBooking;
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
        public Task<List<BookingResponse>> GetBookings();
        public Task<Booking> CreateBooking(BookingRequest booking);
        public Task<bool> UpdateStatusBooking(int bookingId);
        public Task<BookingResponse> GetBookingById(int booking);
        public Task<bool> CreateSlotBookingAndLinkToBooking(CreateScheduleAndSlotBookingRequest createScheduleAndSlot);
        public Task<bool> UpdateBooking(BookingRequest request);
        public Task<bool> DeleteBooking(int bookingId);
    }
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISlotBookingRepository _slotBookingRepository;

        public BookingService(IBookingRepository repo, IMapper mapper, IScheduleRepository scheduleRepository, ISlotBookingRepository slotBookingRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _scheduleRepository = scheduleRepository;
            _slotBookingRepository = slotBookingRepository;
        }

        public async Task<Booking> CreateBooking(BookingRequest request)
        {
            Booking booking = new Booking();
            booking.PetId = request.PetId;
            booking.CustomerId = request.CustomerId;
            booking.BookingDate = DateTime.Now;
            booking.Note = request.Note;
            booking.Status = request.Status;
            return await _repo.CreateBooking(booking);
        }

        public async Task<bool> CreateSlotBookingAndLinkToBooking(CreateScheduleAndSlotBookingRequest createScheduleAndSlot)
        {
            var bookingResponse = _repo.GetBookingById(createScheduleAndSlot.bookingId);
            if (bookingResponse == null)
            {
                throw new Exception("Booking not found");
            }
            Schedule scheduleRequest = new Schedule();
            scheduleRequest.StartTime = createScheduleAndSlot.StartTime;
            scheduleRequest.EndTime = createScheduleAndSlot.EndTime;
            scheduleRequest.Status = true;
            
            var scheduleRespone = await _scheduleRepository.Create(scheduleRequest);
            if (scheduleRespone == null)
            {
                throw new Exception("Failed to create schedule");
            }
            SlotBookingRequest slotBookingRequest = new SlotBookingRequest();
            slotBookingRequest.DoctorId = createScheduleAndSlot.DoctorId;
            slotBookingRequest.ServiceId = createScheduleAndSlot.ServiceId;
            slotBookingRequest.ScheduleId = scheduleRespone.ScheduleId;
           
            var slotBookingResponse = await _slotBookingRepository.Create(slotBookingRequest);

            if (slotBookingResponse == null)
            {
                throw new Exception("Failed to create slot booking");
            }
            var Response = await _repo.LinkBookingToSlotBooking(createScheduleAndSlot.bookingId, slotBookingResponse.SlotBookingId);
            if(Response == false)
            {
                throw new Exception("Failed to create slot booking");
            }
            var updateStatusResponse = await _repo.UpdateStatusBooking(createScheduleAndSlot.bookingId);
            return true;  
         }

        public Task<bool> DeleteBooking(int bookingId)
        {
            return _repo.DeleteBooking(bookingId);
        }

        public async Task<BookingResponse> GetBookingById(int bookingid)
        {
            var item = await _repo.GetBookingById(bookingid);
            if (item == null)
            {
                throw new Exception("Booking not found");
            }
            var booking = new BookingResponse();
            booking.BookingId = item.BookingId;
            booking.PetId = item.PetId;
            booking.CustomerId = item.CustomerId;
            booking.CustomerName = item.Customer.FullName;
            booking.CustomerPhoneNumber = item.Customer.PhoneNumber;
            booking.CustomerEmail = item.Customer.Email;
            booking.CustomerAddress = item.Customer.Address;
            booking.PetName = item.Pet.Name;
            booking.PetSpecies = item.Pet.Species;
            booking.BookingDate = item.BookingDate.Value;
            booking.Note = item.Note;
            booking.Status = item.Status;
            List<SlotBookingResponse> slotBookingResponses = new List<SlotBookingResponse>();
            foreach (var slotBooking in item.SlotBookings)
            {
                SlotBookingResponse slotBookingResponse = new SlotBookingResponse();
                slotBookingResponse.Id = slotBooking.SlotBookingId;
                slotBookingResponse.DoctorId = slotBooking.DoctorId;
                slotBookingResponse.ServiceId = slotBooking.ServiceId;
                slotBookingResponse.ScheduleId = slotBooking.ScheduleId;
                slotBookingResponse.Status = slotBooking.Status.Value;
                slotBookingResponse.DoctorName = slotBooking.Doctor.FullName;
                slotBookingResponse.DoctorPhoneNumber = slotBooking.Doctor.PhoneNumber;
                slotBookingResponse.DoctorEmail = slotBooking.Doctor.Email;
                slotBookingResponse.DoctorSpeciality = slotBooking.Doctor.Speciality;
                slotBookingResponse.ServiceName = slotBooking.Service.ServiceName;
                slotBookingResponse.ServiceDescription = slotBooking.Service.Description;
                slotBookingResponse.Price = slotBooking.Service.Price;
                slotBookingResponse.StartTime = slotBooking.Schedule.StartTime.Value;
                slotBookingResponse.EndTime = slotBooking.Schedule.EndTime.Value;
                slotBookingResponses.Add(slotBookingResponse);
            }
            booking.SlotBookings = slotBookingResponses;

            return booking;
        }

        public async Task<List<BookingResponse>> GetBookings()
        {
            var bookings = await _repo.GetAll();
            var response = new List<BookingResponse>();
            foreach (var item in bookings)
            {
                var booking = new BookingResponse();
                booking.BookingId = item.BookingId;
                booking.PetId = item.PetId;
                booking.CustomerId = item.CustomerId;
                booking.CustomerName = item.Customer.FullName;
                booking.CustomerPhoneNumber = item.Customer.PhoneNumber;
                booking.CustomerEmail = item.Customer.Email;
                booking.CustomerAddress = item.Customer.Address;
                booking.PetName = item.Pet.Name;
                booking.PetSpecies = item.Pet.Species;
                booking.BookingDate = item.BookingDate.Value;
                booking.Note = item.Note;
                booking.Status = item.Status;
                List<SlotBookingResponse> slotBookingResponses = new List<SlotBookingResponse>();
                foreach (var slotBooking in item.SlotBookings)
                {
                    SlotBookingResponse slotBookingResponse = new SlotBookingResponse();
                    slotBookingResponse.Id = slotBooking.SlotBookingId;
                    slotBookingResponse.DoctorId = slotBooking.DoctorId;
                    slotBookingResponse.ServiceId = slotBooking.ServiceId;
                    slotBookingResponse.ScheduleId = slotBooking.ScheduleId;
                    slotBookingResponse.Status = slotBooking.Status.Value;
                    slotBookingResponse.DoctorName = slotBooking.Doctor.FullName;
                    slotBookingResponse.DoctorPhoneNumber = slotBooking.Doctor.PhoneNumber;
                    slotBookingResponse.DoctorEmail = slotBooking.Doctor.Email;
                    slotBookingResponse.DoctorSpeciality = slotBooking.Doctor.Speciality;
                    slotBookingResponse.ServiceName = slotBooking.Service.ServiceName;
                    slotBookingResponse.ServiceDescription = slotBooking.Service.Description;
                    slotBookingResponse.Price = slotBooking.Service.Price;
                    slotBookingResponse.StartTime = slotBooking.Schedule.StartTime.Value;
                    slotBookingResponse.EndTime = slotBooking.Schedule.EndTime.Value;
                    slotBookingResponses.Add(slotBookingResponse);
                }
                booking.SlotBookings = slotBookingResponses;
                response.Add(booking);


            }
            return response;
           

    }

        public Task<bool> UpdateBooking(BookingRequest request)
        {
           var booking = _mapper.Map<Booking>(request);
            return _repo.Update(booking);
        }

        public  async Task<bool> UpdateStatusBooking(int bookingId)
        {
            return await _repo.UpdateStatusBooking(bookingId);
        }


    }
}
