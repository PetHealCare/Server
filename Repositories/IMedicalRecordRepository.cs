using BusinessObjects.Models;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public interface IMedicalRecordRepository
    {
        public Task<List<MedicalRecord>> GetAll();
        public Task<MedicalRecord> GetOne(int id);
        public Task<MedicalRecord> Create(MedicalRecord record);
        public Task<bool> Update(int id, MedicalRecord record);
        public Task<bool> Delete(int id);
    }

    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        public Task<MedicalRecord> Create(MedicalRecord record)
        {
            return MedicalRecordDAO.Instance.CreateMedical(record);
        }

        public Task<bool> Delete(int id)
        {
            return MedicalRecordDAO.Instance.DeleteMedical(id);
        }

        public Task<List<MedicalRecord>> GetAll()
        {
            return MedicalRecordDAO.Instance.GetAll();
        }

        public Task<MedicalRecord> GetOne(int id)
        {
            return MedicalRecordDAO.Instance.GetOne(id);
        }

        public Task<bool> Update(int id, MedicalRecord record)
        {
            return MedicalRecordDAO.Instance.UpdateMedical(id, record);
        }
    }
}
