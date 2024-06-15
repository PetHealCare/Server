using BusinessObjects.Models;
using DataAccessLayers;
using DTOs.Request.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IScheduleRepository
    {
        
        public Task<bool> Update(Schedule schedule);
        public Task<bool> Delete(int id);
        public Task<Schedule> Get(int id);
        public Task<List<Schedule>> GetAll();
        Task<Schedule> Create(Schedule schedule);
        public Task<bool> updateStatus(int id);


    }
    public class ScheduleRepository : IScheduleRepository
    {
        public async Task<Schedule> Create(Schedule schedule)
        {
            return await ScheduleDAO.Instance.Create(schedule);
        }

       

        public async Task<bool> Delete(int id)
        {
            return  ScheduleDAO.Instance.Delete(id);
        }

        public async Task<Schedule> Get(int id)
        {
           return ScheduleDAO.Instance.GetById(id);
        }

        public async Task<List<Schedule>> GetAll()
        {
           return ScheduleDAO.Instance.GetAll();
        }

        public async Task<bool> Update(Schedule schedule)
        {
            return await ScheduleDAO.Instance.UpdateSchedule(schedule);
        }

        public async Task<bool> updateStatus(int id)
        {
            return await ScheduleDAO.Instance.updateStatus(id);
        }

    }
}
