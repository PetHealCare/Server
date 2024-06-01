using BusinessObjects.Models;
using DTOs.Request.Booking;
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

       

    }
}
