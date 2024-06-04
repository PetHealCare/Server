using BusinessObjects.Models;
using DTOs.Request.Schedule;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IScheduleService
    {
        public Task<Schedule> Create(ScheduleRequest schedule);
        public Task<bool> Update(Schedule schedule);
        public Task<Schedule> Get(int id);
        public Task<List<Schedule>> GetAll();
        public Task<bool> Delete(int id);
        
    }
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Schedule> Create(ScheduleRequest schedule)
        {
            return await _scheduleRepository.Create(schedule);
        }

        public async Task<bool> Delete(int id)
        {
            return await _scheduleRepository.Delete(id);
        }

        public async Task<Schedule> Get(int id)
        {
            return await _scheduleRepository.Get(id);
        }

        public async Task<List<Schedule>> GetAll()
        {
            return await _scheduleRepository.GetAll();
        }

        public async Task<bool> Update(Schedule schedule)
        {
            return await _scheduleRepository.Update(schedule);
        }
    }
}
