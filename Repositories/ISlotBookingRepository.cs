using BusinessObjects.Models;
using DataAccessLayers;
using DTOs.Request.Booking;
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
        public Task<List<SlotBooking>> GetAll();
        public Task<SlotBooking> GetListSlotBooking(int? doctorId = null, int? serviceId = null, int? scheduleId = null);
        public Task<bool> Update(SlotBooking slotBooking);
        public Task<SlotBooking> GetSlotBooking(int id);
        public bool Delete(int id);
    }

    public class SlotBookingRepository : ISlotBookingRepository
    {
        public async Task<SlotBooking> Create(SlotBookingRequest request)
        {
            return await SlotBookingDAO.Instance.Create(request);
        }

        public bool Delete(int id)
        {
           return SlotBookingDAO.Instance.Delete(id);
        }

        public async Task<List<SlotBooking>> GetAll()
        {
            return SlotBookingDAO.Instance.GetListSlotBooking();
        }

        public async Task<SlotBooking> GetListSlotBooking(int? doctorId = null, int? serviceId = null, int? scheduleId = null)
        {
            return SlotBookingDAO.Instance.GetListSlotBooking(doctorId, serviceId, scheduleId);
        }

        public async Task<SlotBooking> GetSlotBooking(int id)
        {
            return  SlotBookingDAO.Instance.GetSlotBooking(id);
        }

        public async Task<bool> Update(SlotBooking slotBooking)
        {
            return await SlotBookingDAO.Instance.Update(slotBooking);
        }
    }
}
