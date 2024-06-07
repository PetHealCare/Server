using BusinessObjects.Models;
using DTOs;
using DTOs.Response.SlotBooking;
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
                BookingDate = b.BookingDate,    
                Note = b.Note,
                Status = b.Status,
                Customer = new Customer
                {
                    CustomerId = b.Customer.CustomerId,
                    FullName = b.Customer.FullName,
                    PhoneNumber = b.Customer.PhoneNumber,
                    Email = b.Customer.Email,
                    Address = b.Customer.Address
                },
                Pet = new Pet
                {
                    PetId = b.Pet.PetId,
                    Name = b.Pet.Name,
                    Species = b.Pet.Species
                },
                SlotBookings = b.SlotBookings.Select(sb => new SlotBooking
                {
                    SlotBookingId = sb.SlotBookingId,
                    DoctorId = sb.DoctorId,
                    ServiceId = sb.ServiceId,
                    ScheduleId = sb.ScheduleId,
                    Status = sb.Status,
                    Doctor = new Doctor
                    {
                        DoctorId = sb.Doctor.DoctorId,
                        FullName = sb.Doctor.FullName,
                        PhoneNumber = sb.Doctor.PhoneNumber,
                        Email = sb.Doctor.Email,
                        Speciality = sb.Doctor.Speciality
                    },
                    Schedule = new Schedule
                    {
                        ScheduleId = sb.Schedule.ScheduleId,
                        StartTime = sb.Schedule.StartTime,
                        EndTime = sb.Schedule.EndTime
                    },
                    Service = new Service
                    {
                        ServiceId = sb.Service.ServiceId,
                        ServiceName = sb.Service.ServiceName,
                        Price = sb.Service.Price,
                        Description = sb.Service.Description
                    }
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
                BookingDate = b.BookingDate,
                Note = b.Note,
                Status = b.Status,
                Customer = new Customer
                {
                    CustomerId = b.Customer.CustomerId,
                    FullName = b.Customer.FullName,
                    PhoneNumber = b.Customer.PhoneNumber,
                    Email = b.Customer.Email,
                    Address = b.Customer.Address
                },
                Pet = new Pet
                {
                    PetId = b.Pet.PetId,
                    Name = b.Pet.Name,
                    Species = b.Pet.Species
                },
                SlotBookings = b.SlotBookings.Select(sb => new SlotBooking
                {
                    SlotBookingId = sb.SlotBookingId,
                    DoctorId = sb.DoctorId,
                    ServiceId = sb.ServiceId,
                    ScheduleId = sb.ScheduleId,
                    Status = sb.Status,
                    Doctor = new Doctor
                    {
                        DoctorId = sb.Doctor.DoctorId,
                        FullName = sb.Doctor.FullName,
                        PhoneNumber = sb.Doctor.PhoneNumber,
                        Email = sb.Doctor.Email,
                        Speciality = sb.Doctor.Speciality
                    },
                    Schedule = new Schedule
                    {
                        ScheduleId = sb.Schedule.ScheduleId,
                        StartTime = sb.Schedule.StartTime,
                        EndTime = sb.Schedule.EndTime
                    },
                    Service = new Service
                    {
                        ServiceId = sb.Service.ServiceId,
                        ServiceName = sb.Service.ServiceName,
                        Price = sb.Service.Price,
                        Description = sb.Service.Description
                    }
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

        public async Task<bool> LinkBookingToSlotBooking(int bookingId, int slotBookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            var slotBooking = await _context.SlotBookings.FindAsync(slotBookingId);

            if (booking != null && slotBooking != null)
            {
                booking.SlotBookings.Add(slotBooking);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
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
