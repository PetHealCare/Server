using BusinessObjects.Models;
using DTOs;
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
    }
}
