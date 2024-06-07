using BusinessObjects.Models;
using DataAccessLayers;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingRepository
    {
        public Task<List<Booking>> GetAll();
        public Task<Booking> CreateBooking(Booking booking);

        public Task<bool> UpdateStatusBooking(int bookingId);
        public Task<bool> Update(Booking booking);

        public Task<Booking> GetBookingById(int booking);
        public Task<bool> LinkBookingToSlotBooking(int bookingId, int slotBookingId);
        public Task<bool> DeleteBooking(int bookingId);
    
    }
    public class BookingRepository : IBookingRepository
    {
        public async Task<Booking> CreateBooking(Booking booking)
        {
           
            return  BookingDAO.Instance.Create(booking);
        }

        public Task<bool> DeleteBooking(int bookingId)
        {
           return BookingDAO.Instance.Delete(bookingId);
        }

        public async Task<List<Booking>> GetAll()
        {
            return await BookingDAO.Instance.GetBookings();
        }

        public async Task<Booking> GetBookingById(int booking)
        {
            return  await BookingDAO.Instance.GetBookingById(booking);
        }

        public async Task<bool> LinkBookingToSlotBooking(int bookingId, int slotBookingId)
        {
            return await BookingDAO.Instance.LinkBookingToSlotBooking(bookingId, slotBookingId);
        }

        public async Task<bool> Update(Booking booking)
        {
            return await BookingDAO.Instance.Update(booking);
        }

        public async Task<bool> UpdateStatusBooking(int bookingId)
        {
            return  await BookingDAO.Instance.UpdateStatusBooking(bookingId);
        }

        
    }
}
