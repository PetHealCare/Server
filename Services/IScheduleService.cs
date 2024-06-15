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
        public Task<List<ScheduleResponse>> GetAll(GetListScheduleRequest request);
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
            schedule.DoctorId = request.DoctorId;
            schedule.RoomNo = request.RoomNo;
            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;
            schedule.SlotBooking = request.SlotBooking;
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
            scheduleResponse.RoomNo = schedule.RoomNo;
            scheduleResponse.EndTime = schedule.EndTime;
            scheduleResponse.SlotBooking = schedule.SlotBooking;
            scheduleResponse.Status = schedule.Status;
            return scheduleResponse;
           
        }

        public async Task<List<ScheduleResponse>> GetAll(GetListScheduleRequest request)
        {
            var schedulesQuery = (await _scheduleRepository.GetAll()).AsQueryable();

      

            if (!string.IsNullOrEmpty(request.Status.ToString()))
            {
                schedulesQuery = schedulesQuery.Where(p => p.Status == true);
            }
            if (request.DoctorId != 0)
            {
                schedulesQuery = schedulesQuery.Where(p => p.DoctorId == request.DoctorId);
            }
            if(!string.IsNullOrEmpty(request.RoomNo))
            {
                schedulesQuery = schedulesQuery.Where(s => s.RoomNo.Contains(request.RoomNo));
            }
            var schedules = schedulesQuery.ToList();
            
            if (schedules == null)
            {
                throw new Exception("Failed to get schedules");
            }
            var scheduleResponses = new List<ScheduleResponse>();
            foreach (var schedule in schedules)
            {
                ScheduleResponse scheduleResponse = new ScheduleResponse();
                scheduleResponse.ScheduleId = schedule.ScheduleId;
                scheduleResponse.DoctorId = schedule.DoctorId;
                scheduleResponse.RoomNo = schedule.RoomNo;
                scheduleResponse.StartTime = schedule.StartTime;
                scheduleResponse.EndTime = schedule.EndTime;
                scheduleResponse.SlotBooking = schedule.SlotBooking;
                scheduleResponse.Status = schedule.Status;
                scheduleResponses.Add(scheduleResponse);
            }
            return scheduleResponses;
        }

        public async Task<bool> Update(ScheduleRequest request)
        {
            var schedule = new Schedule();
            schedule.ScheduleId = request.ScheduleId;
            schedule.DoctorId = request.DoctorId;
            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;
            schedule.RoomNo = request.RoomNo;
            schedule.Status = request.Status;
            schedule.SlotBooking = request.SlotBooking;
            return await _scheduleRepository.Update(schedule);
        }
    }
}
