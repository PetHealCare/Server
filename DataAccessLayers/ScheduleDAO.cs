using BusinessObjects.Models;
using DTOs.Request.Schedule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class ScheduleDAO : GenericDAO<Schedule>
    {
        private static readonly Lazy<ScheduleDAO> _instance =
        new Lazy<ScheduleDAO>(() => new ScheduleDAO(new PetHealthCareContext()));
        public static ScheduleDAO Instance => _instance.Value;
        public ScheduleDAO(PetHealthCareContext context) : base(context)
    {

    }
        public async Task<bool> updateStatus(int id)
        {
            var schedule = GetById(id);
            if (schedule == null)
            {
                return false;
            }
            schedule.Status = !schedule.Status;
            _context.Schedules.Update(schedule);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSchedule(Schedule schedule)
        {
            var scheduleUpdate = _context.Schedules.FirstOrDefault(s => s.ScheduleId == schedule.ScheduleId);
            if (scheduleUpdate == null)
            {
                return false;
            }
            scheduleUpdate.RoomNo = schedule.RoomNo;
            scheduleUpdate.StartTime = schedule.StartTime;
            scheduleUpdate.EndTime = schedule.EndTime;
            scheduleUpdate.Status = schedule.Status;
            scheduleUpdate.SlotBooking = schedule.SlotBooking;
             _context.Schedules.Update(scheduleUpdate);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Schedule> Create(Schedule schedule)
        {
            
            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }
        public async Task<List<Schedule>> GetSchedulesByDoctorId(int doctorId)
        {
            return _context.Schedules
            .Where(s => s.DoctorId == doctorId)
            .ToList();
        }
}
}
