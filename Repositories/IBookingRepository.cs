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
        public Task<Booking> CreateBooking(Booking booking);

        public Task<bool> UpdateStatusBooking(int bookingId);

        public Booking GetBookingById(int booking);
        public Task<bool> LinkBookingToSlotBooking(int bookingId, int slotBookingId);
    
    }
    public class BookingRepository : IBookingRepository
    {
        public async Task<Booking> CreateBooking(Booking booking)
        {
           
            return  BookingDAO.Instance.Create(booking);
        }

        public Booking GetBookingById(int booking)
        {
            return  BookingDAO.Instance.GetById(booking);
        }

        public async Task<bool> LinkBookingToSlotBooking(int bookingId, int slotBookingId)
        {
            return await BookingDAO.Instance.LinkBookingToSlotBooking(bookingId, slotBookingId);
        }

        public async Task<bool> UpdateStatusBooking(int bookingId)
        {
            return  await BookingDAO.Instance.UpdateStatusBooking(bookingId);
        }
    }
}
