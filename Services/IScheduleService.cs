using BusinessObjects.Models;
using DTOs.Request.Schedule;
using DTOs.Response.Schedule;
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
        public Task<bool> Update(ScheduleRequest schedule);
        public Task<ScheduleResponse> Get(int id);
        public Task<List<ScheduleResponse>> GetAll();
        public Task<bool> Delete(int id);
        
    }
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Schedule> Create(ScheduleRequest request)
        {
            var schedule = new Schedule();
            schedule.ScheduleId = request.ScheduleId;
            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;
            schedule.Status = request.Status;


            return await _scheduleRepository.Create(schedule);
        }

        public async Task<bool> Delete(int id)
        {
            return await _scheduleRepository.Delete(id);
        }

        public async Task<ScheduleResponse> Get(int id)
        {
            var schedule = await _scheduleRepository.Get(id);
            if (schedule == null)
            {
                throw new Exception("Schedule not found");
            }
            ScheduleResponse scheduleResponse = new ScheduleResponse();
            scheduleResponse.ScheduleId = schedule.ScheduleId;
            scheduleResponse.StartTime = schedule.StartTime;
            scheduleResponse.EndTime = schedule.EndTime;
            scheduleResponse.Status = schedule.Status;
            return scheduleResponse;
           
        }

        public async Task<List<ScheduleResponse>> GetAll()
        {
            var schedules = await _scheduleRepository.GetAll();
            if (schedules == null)
            {
                throw new Exception("Failed to get schedules");
            }
            var scheduleResponses = new List<ScheduleResponse>();
            foreach (var schedule in schedules)
            {
                ScheduleResponse scheduleResponse = new ScheduleResponse();
                scheduleResponse.ScheduleId = schedule.ScheduleId;
                scheduleResponse.StartTime = schedule.StartTime;
                scheduleResponse.EndTime = schedule.EndTime;
                scheduleResponse.Status = schedule.Status;
                scheduleResponses.Add(scheduleResponse);
            }
            return scheduleResponses;
        }

        public async Task<bool> Update(ScheduleRequest request)
        {
            var schedule = new Schedule();
            schedule.ScheduleId = request.ScheduleId;
            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;
            schedule.Status = request.Status;
            return await _scheduleRepository.Update(schedule);
        }
    }
}
