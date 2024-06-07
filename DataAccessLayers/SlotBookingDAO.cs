using BusinessObjects.Models;
using DTOs.Request.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class SlotBookingDAO : GenericDAO<SlotBooking>
    {
        private static readonly Lazy<SlotBookingDAO> _instance =
        new Lazy<SlotBookingDAO>(() => new SlotBookingDAO(new PetHealthCareContext()));
        public static SlotBookingDAO Instance => _instance.Value;
        public SlotBookingDAO(PetHealthCareContext context) : base(context)
        {

        }
        public List<SlotBooking> GetListSlotBooking()
        {
            return  _context.SlotBookings.Select(s => new SlotBooking
            {
                SlotBookingId = s.SlotBookingId,
                DoctorId = s.DoctorId,
                ServiceId = s.ServiceId,
                ScheduleId = s.ScheduleId,
                Status = s.Status,
                Doctor = new Doctor
                {
                    DoctorId = s.Doctor.DoctorId,
                    FullName = s.Doctor.FullName,
                    PhoneNumber = s.Doctor.PhoneNumber,
                    Email = s.Doctor.Email,
                    Speciality = s.Doctor.Speciality,

                },
                Schedule = new Schedule
                {
                    ScheduleId = s.Schedule.ScheduleId,
                    StartTime = s.Schedule.StartTime,
                    EndTime = s.Schedule.EndTime,
                    
                },
                Service = new Service
                {
                    ServiceId = s.Service.ServiceId,
                    ServiceName = s.Service.ServiceName,
                    Price = s.Service.Price,
                    Description = s.Service.Description
                }
                
            }).
                
                ToList();
        }

        public SlotBooking GetSlotBooking(int id)
        {
            return _context.SlotBookings
       .Where(s => s.SlotBookingId == id)
       .Select(s => new SlotBooking
       {
           SlotBookingId = s.SlotBookingId,
           DoctorId = s.DoctorId,
           ServiceId = s.ServiceId,
           ScheduleId = s.ScheduleId,
           Status = s.Status,
           Doctor = new Doctor
           {
               DoctorId = s.Doctor.DoctorId,
               FullName = s.Doctor.FullName,
               PhoneNumber = s.Doctor.PhoneNumber,
               Email = s.Doctor.Email,
               Speciality = s.Doctor.Speciality,
           },
           Schedule = new Schedule
           {
               ScheduleId = s.Schedule.ScheduleId,
               StartTime = s.Schedule.StartTime,
               EndTime = s.Schedule.EndTime,
           },
           Service = new Service
           {
               ServiceId = s.Service.ServiceId,
               ServiceName = s.Service.ServiceName,
               Price = s.Service.Price,
               Description = s.Service.Description
           }
       })
       .FirstOrDefault();
        }

        public SlotBooking GetListSlotBooking(int? doctorId = null, int? serviceId = null, int? scheduleId = null)
        {
            var query = _context.SlotBookings.AsQueryable();

            if (doctorId.HasValue)
            {
                query = query.Where(s => s.DoctorId == doctorId.Value);
            }

            if (serviceId.HasValue)
            {
                query = query.Where(s => s.ServiceId == serviceId.Value);
            }

            if (scheduleId.HasValue)
            {
                query = query.Where(s => s.ScheduleId == scheduleId.Value);
            }

            return query.Select(s => new SlotBooking
            {
                SlotBookingId = s.SlotBookingId,
                DoctorId = s.DoctorId,
                ServiceId = s.ServiceId,
                ScheduleId = s.ScheduleId,
                Status = s.Status,
                Doctor = new Doctor
                {
                    DoctorId = s.Doctor.DoctorId,
                    FullName = s.Doctor.FullName,
                    PhoneNumber = s.Doctor.PhoneNumber,
                    Email = s.Doctor.Email,
                    Speciality = s.Doctor.Speciality,
                },
                Schedule = new Schedule
                {
                    ScheduleId = s.Schedule.ScheduleId,
                    StartTime = s.Schedule.StartTime,
                    EndTime = s.Schedule.EndTime,
                },
                Service = new Service
                {
                    ServiceId = s.Service.ServiceId,
                    ServiceName = s.Service.ServiceName,
                    Price = s.Service.Price,
                    Description = s.Service.Description,
                }
            }).FirstOrDefault();
        }

        public async Task<SlotBooking> Create(SlotBookingRequest request)
        {
            SlotBooking booking = new SlotBooking();
            booking.DoctorId = request.DoctorId;
            booking.ServiceId = request.ServiceId;
            booking.ScheduleId = request.ScheduleId;
            booking.Status = true;
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
        public async Task<bool> Update(SlotBooking request)
        {
            var slotBookingInDb = await _context.SlotBookings.FindAsync(request.SlotBookingId);
            if (slotBookingInDb == null)
            {
                return false;
            }
            slotBookingInDb.DoctorId = request.DoctorId;
            slotBookingInDb.ServiceId = request.ServiceId;
            slotBookingInDb.ScheduleId = request.ScheduleId;
            slotBookingInDb.Status = request.Status;
            _context.SlotBookings.Update(slotBookingInDb);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
