using BusinessObjects.Models;
using DTOs.Request.Booking;
using DTOs.Response.SlotBooking;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISlotBookingService
    {
        public Task<SlotBooking> Create(SlotBookingRequest request);
        public Task<List<SlotBookingResponse>> GetAll();
        public SlotBookingResponse GetListSlotBooking(int? doctorId = null, int? serviceId = null, int? scheduleId = null);
        public bool Update(SlotBooking slotBooking);
        public bool Delete(int id);
    }
    public class SlotBookingService : ISlotBookingService
    {
        private readonly ISlotBookingRepository _slotBookingRepository;
        
        public SlotBookingService(ISlotBookingRepository slotBookingRepository)
        {
            _slotBookingRepository = slotBookingRepository;
         
        }

        public async Task<SlotBooking> Create(SlotBookingRequest request)
        {
            var slotBooking = await _slotBookingRepository.Create(request);
            if (slotBooking == null)
            {
                throw new Exception("Failed to create slot booking");
            }


            return slotBooking;
        }

        public bool Delete(int id)
        {
            return _slotBookingRepository.Delete(id);
        }

        public async Task<List<SlotBookingResponse>> GetAll()
        {
            var slotBookings = await _slotBookingRepository.GetAll();
            if (slotBookings == null)
            {
                throw new Exception("Failed to get slot bookings");
            }
            
            List<SlotBookingResponse> slotBookingResponses = new List<SlotBookingResponse>();
            foreach (var slotBooking in slotBookings)
            {
                SlotBookingResponse slotBookingResponse = new SlotBookingResponse();
                slotBookingResponse.Id = slotBooking.SlotBookingId;
                slotBookingResponse.DoctorId = slotBooking.DoctorId;
                slotBookingResponse.ServiceId = slotBooking.ServiceId;
                slotBookingResponse.ScheduleId = slotBooking.ScheduleId;
                slotBookingResponse.DoctorName = slotBooking.Doctor.FullName;
                slotBookingResponse.DoctorPhoneNumber = slotBooking.Doctor.PhoneNumber;
                slotBookingResponse.DoctorEmail = slotBooking.Doctor.Email;
                slotBookingResponse.DoctorSpeciality = slotBooking.Doctor.Speciality;
                slotBookingResponse.ServiceName = slotBooking.Service.ServiceName;
                slotBookingResponse.ServiceDescription = slotBooking.Service.Description;
                slotBookingResponse.Price = slotBooking.Service.Price;
                slotBookingResponse.StartTime = slotBooking.Schedule.StartTime.Value;
                slotBookingResponse.EndTime = slotBooking.Schedule.EndTime.Value;
                slotBookingResponse.Status = slotBooking.Status.Value;
                slotBookingResponses.Add(slotBookingResponse);
            }
            return  slotBookingResponses;
        }

        public SlotBookingResponse GetListSlotBooking(int? doctorId = null, int? serviceId = null, int? scheduleId = null)
        {
           var response = _slotBookingRepository.GetListSlotBooking(doctorId, serviceId, scheduleId);
            if (response == null)
            {
                throw new Exception("Failed to get slot booking");
            }
            SlotBookingResponse slotBookingResponse = new SlotBookingResponse();
            slotBookingResponse.Id = response.SlotBookingId;
            slotBookingResponse.DoctorId = response.DoctorId;
            slotBookingResponse.ServiceId = response.ServiceId;
            slotBookingResponse.ScheduleId = response.ScheduleId;
            slotBookingResponse.DoctorName = response.Doctor.FullName;
            slotBookingResponse.DoctorPhoneNumber = response.Doctor.PhoneNumber;
            slotBookingResponse.DoctorEmail = response.Doctor.Email;
            slotBookingResponse.DoctorSpeciality = response.Doctor.Speciality;
            slotBookingResponse.ServiceName = response.Service.ServiceName;
            slotBookingResponse.ServiceDescription = response.Service.Description;
            slotBookingResponse.Price = response.Service.Price;
            slotBookingResponse.StartTime = response.Schedule.StartTime.Value;
            slotBookingResponse.EndTime = response.Schedule.EndTime.Value;
            slotBookingResponse.Status = response.Status.Value;
            return slotBookingResponse;
        }

        public bool Update(SlotBooking slotBooking)
        {
            return _slotBookingRepository.Update(slotBooking);
        }
    }
}
