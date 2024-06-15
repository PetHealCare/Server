using BusinessObjects.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class BookingDAO : GenericDAO<Booking>
    {
        private static readonly Lazy<BookingDAO> _instance =
        new Lazy<BookingDAO>(() => new BookingDAO(new PetHealthCareContext()));
        public static BookingDAO Instance => _instance.Value;
        public BookingDAO(PetHealthCareContext context) : base(context)
        {

        }
       
        public Task<List<Booking>> GetBookings()
        {
            return _context.Bookings.Select(b => new Booking
            {
                BookingId = b.BookingId,
                CustomerId = b.CustomerId,
                PetId = b.PetId,
                DoctorId = b.DoctorId,
                ScheduleId = b.ScheduleId,
                BookingDate = b.BookingDate,   
                Slot = b.Slot,
                Note = b.Note,
                Status = b.Status,
                Customer = new Customer
                {
                    CustomerId = b.Customer.CustomerId,
                    FullName = b.Customer.FullName,
                    PhoneNumber = b.Customer.PhoneNumber,
                    Address = b.Customer.Address,
                    Status = b.Customer.Status,
                    UserId = b.Customer.UserId
                },
                Pet = new Pet
                {
                    PetId = b.Pet.PetId,
                    Name = b.Pet.Name,
                    Species = b.Pet.Species,
                    Status = b.Pet.Status,
                    CustomerId = b.Pet.CustomerId,
                    Age = b.Pet.Age,
                    Gender = b.Pet.Gender,
                    Generic = b.Pet.Generic,
                    Description = b.Pet.Description
                },
                Doctor = new Doctor
                {
                    DoctorId = b.Doctor.DoctorId,
                    FullName = b.Doctor.FullName,
                    PhoneNumber = b.Doctor.PhoneNumber,
                    Speciality = b.Doctor.Speciality,
                    Status = b.Doctor.Status,
                    UserId = b.Doctor.UserId


                },
                Schedule = new Schedule
                {
                    ScheduleId = b.Schedule.ScheduleId,
                    DoctorId = b.Schedule.DoctorId,
                    StartTime = b.Schedule.StartTime,
                    EndTime = b.Schedule.EndTime,
                    RoomNo = b.Schedule.RoomNo,
                    SlotBooking = b.Schedule.SlotBooking,
                    Status = b.Schedule.Status

                },
                Services = b.Services.Select(s => new Service
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    Price = s.Price,
                    Description = s.Description,
                    LimitTime = s.LimitTime
                    

                }).ToList()

                
                


            }).ToListAsync();

           
    }
        public async Task<Booking> GetBookingById(int booking)
        {
            return _context.Bookings.Select(b => new Booking
            {
                BookingId = b.BookingId,
                CustomerId = b.CustomerId,
                PetId = b.PetId,
                DoctorId = b.DoctorId,
                ScheduleId = b.ScheduleId,
                BookingDate = b.BookingDate,
                Slot = b.Slot,
                Note = b.Note,
                Status = b.Status,
                Customer = new Customer
                {
                    CustomerId = b.Customer.CustomerId,
                    FullName = b.Customer.FullName,
                    PhoneNumber = b.Customer.PhoneNumber,
                    Address = b.Customer.Address,
                    Status = b.Customer.Status,
                    UserId = b.Customer.UserId
                },
                Pet = new Pet
                {
                    PetId = b.Pet.PetId,
                    Name = b.Pet.Name,
                    Species = b.Pet.Species,
                    Status = b.Pet.Status,
                    CustomerId = b.Pet.CustomerId,
                    Age = b.Pet.Age,
                    Gender =  b.Pet.Gender,
                    Generic = b.Pet.Generic,
                    Description = b.Pet.Description
                },
                Doctor = new Doctor
                {
                    DoctorId = b.Doctor.DoctorId,
                    FullName = b.Doctor.FullName,
                    PhoneNumber = b.Doctor.PhoneNumber,
                    Speciality = b.Doctor.Speciality,
                    Status = b.Doctor.Status,
                    UserId = b.Doctor.UserId


                },
                Schedule = new Schedule
                {
                    ScheduleId = b.Schedule.ScheduleId,
                    DoctorId = b.Schedule.DoctorId,
                    
                    StartTime = b.Schedule.StartTime,
                    EndTime = b.Schedule.EndTime,
                    RoomNo = b.Schedule.RoomNo,
                    SlotBooking = b.Schedule.SlotBooking,
                    Status = b.Schedule.Status

                },
                
                Services = b.Services.Select(s => new Service
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    Price = s.Price,
                    Description = s.Description,
                    LimitTime = s.LimitTime


                }).ToList()





            }).FirstOrDefault();



            
        }
        public async Task<bool> Update(Booking booking)
        {
            var bookingInDb = await _context.Bookings.FindAsync(booking.BookingId);
            if (bookingInDb == null)
            {
                return false;
            }
             
            bookingInDb.PetId = booking.PetId;
            bookingInDb.CustomerId = booking.CustomerId;
            bookingInDb.BookingDate = booking.BookingDate;
            bookingInDb.Note = booking.Note;
            bookingInDb.Status = booking.Status;
            _context.Bookings.Update(bookingInDb);
            
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateStatusBooking(int bookingId)
        {
            Booking booking = _context.Bookings.Find(bookingId);
            if (booking == null)
            {
                return false;
            }
            booking.Status = true;
            await _context.SaveChangesAsync();
            return true;
        }

       

        public async Task<bool> Delete(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking == null)
            {
                return false;
            }
             _context.Bookings.Remove(booking);
            return  _context.SaveChanges() > 0;
        }

    }
}
