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
    public interface IScheduleRepository
    {
        public Task<Schedule> Create(ScheduleRequest schedule);
    }
    public class ScheduleRepository : IScheduleRepository
    {
        public async Task<Schedule> Create(ScheduleRequest schedule)
        {
            return await ScheduleDAO.Instance.Create(schedule);
        }
    }
}
