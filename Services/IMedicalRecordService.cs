using BusinessObjects.Models;
using DTOs.Request.MedicalRecord;
using DTOs.Response.MedicalRecordResponse;
using Presentation.Client;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMedicalRecordService
    {
        public Task<List<MedicalRecordResponse>> GetAll(GetListMedicalRecordRequest request);
        public Task<MedicalRecordResponse> GetOne(int id);
        public Task<MedicalRecord> Create(MedicalRecordRequest record);
        public Task<bool> Update(int id, MedicalRecordRequest record);
        public Task<bool> Delete(int id);
    }

    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repo;
        private readonly OdataClient _client;
        public MedicalRecordService(IMedicalRecordRepository repo, OdataClient client)
        {
            _repo = repo;
            _client = client;
        }
        public async Task<MedicalRecord> Create(MedicalRecordRequest record)
        {
            var request = new MedicalRecord();
            request.PetId = record.PetId;
            request.DoctorId = record.DoctorId;
            request.VisitDate = record.VisitDate;
            request.Treatment = record.Treatment;
            request.Notes = record.Notes;
            return await _repo.Create(request);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public async Task<List<MedicalRecordResponse>> GetAll(GetListMedicalRecordRequest request)
        {
            var medicals = (await _repo.GetAll()).AsQueryable();
            //var medicals = (await _odataClient.GetBookingsAsync()).AsQueryable();
            if (request.PetId != 0)
            {
                medicals = medicals.Where(b => b.PetId == request.PetId);
            }
            if (request.DoctorId != 0)
            {
                medicals = medicals.Where(b => b.DoctorId == request.DoctorId);
            }
            var response = new List<MedicalRecordResponse>();
            foreach (var item in medicals)
            {
                var medical = new MedicalRecordResponse();
                medical.RecordId = item.RecordId;
                medical.PetId = item.PetId;
                medical.DoctorId = item.DoctorId;
                medical.VisitDate = item.VisitDate;
                medical.Diagnosis = item.Diagnosis;
                medical.Treatment = item.Treatment;
                medical.Notes = item.Notes;
                response.Add(medical);
            }
            return response;
        }

        public async Task<MedicalRecordResponse> GetOne(int id)
        {
            
            return await _client.GetMedicalByIdAsync(id);
        }

        public Task<bool> Update(int id, MedicalRecordRequest record)
        {
            var medical = new MedicalRecord();
            medical.PetId = record.PetId;
            medical.DoctorId = record.DoctorId;
            medical.VisitDate = record.VisitDate;
            medical.Diagnosis = record.Diagnosis;
            medical.Treatment = record.Treatment;
            medical.Notes = record.Notes;
            return _repo.Update(id, medical);
        }
    }
}
