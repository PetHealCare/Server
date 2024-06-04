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
    

        public async Task<Schedule> Create(ScheduleRequest request)
        {
            Schedule schedule = new Schedule();
            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;
            schedule.Status = true;
            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }
}
}
