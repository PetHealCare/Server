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
    public interface ISlotBookingRepository
    {
        public Task<SlotBooking> Create(SlotBookingRequest request);
    }

    public class SlotBookingRepository : ISlotBookingRepository
    {
        public async Task<SlotBooking> Create(SlotBookingRequest request)
        {
            return await SlotBookingDAO.Instance.Create(request);
        }
    }
}
