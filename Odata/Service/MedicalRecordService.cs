using DTOs.Request.MedicalRecord;
using DTOs.Response.MedicalRecordResponse;
using Repositories;

namespace Odata.Service
{
    public interface IMedicalRecordSerivce
    {
        public Task<List<MedicalRecordResponse>> GetAll();
        public Task<MedicalRecordResponse> GetOne(int id);
    }
    public class MedicalRecordService : IMedicalRecordSerivce
    {
        private readonly IMedicalRecordRepository _repo;
        public MedicalRecordService(IMedicalRecordRepository repo)
        {
               _repo = repo;
        }

        public async Task<List<MedicalRecordResponse>> GetAll()
        {
            var medicals = await _repo.GetAll();
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
            var medical = await _repo.GetOne(id);
            var response = new MedicalRecordResponse();
            response.RecordId = medical.RecordId;
            response.PetId = medical.PetId;
            response.DoctorId = medical.DoctorId;
            response.VisitDate = medical.VisitDate;
            response.Diagnosis = medical.Diagnosis;
            response.Treatment = medical.Treatment;
            response.Notes = medical.Notes;
            return response;
        }
    }
}
