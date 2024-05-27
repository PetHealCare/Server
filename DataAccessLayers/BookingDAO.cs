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
        public async Task<Booking> CreateBooking(BookingRequest request)
        {
            Booking booking = new Booking();
            booking.PetId = request.PetId;
            booking.CustomerId = request.CustomerId;
            booking.BookingDate = DateTime.Now;
            booking.Note = request.Note;
            booking.Status = false;
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
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

    }
}
