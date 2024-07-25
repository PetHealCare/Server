using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class MedicalRecordDAO
    {
        private static readonly Lazy<MedicalRecordDAO> _instance =
        new Lazy<MedicalRecordDAO>(() => new MedicalRecordDAO(new PetHealthCareContext()));
        public static MedicalRecordDAO Instance => _instance.Value;
        private PetHealthCareContext _context;
        public MedicalRecordDAO(PetHealthCareContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalRecord>> GetAll()
        {
            return _context.MedicalRecords.ToList();

        }

        public async Task<MedicalRecord> GetOne(int id)
        {
            return _context.MedicalRecords.Where(m => m.RecordId == id).FirstOrDefault();
        }

        public async Task<MedicalRecord> CreateMedical(MedicalRecord record)
        {
            _context.MedicalRecords.Add(record);
            _context.SaveChanges();

            return record;
        }

        public async Task<bool> UpdateMedical(int id, MedicalRecord record)
        {
            var exitingMedical = _context.MedicalRecords.Where(m => m.RecordId == id).FirstOrDefault();
            if (exitingMedical != null)
            {
                exitingMedical.VisitDate = record.VisitDate;
                exitingMedical.Diagnosis = record.Diagnosis;
                exitingMedical.Treatment = record.Treatment;
                exitingMedical.Notes = record.Notes;
                _context.MedicalRecords.Update(exitingMedical);
               return _context.SaveChanges() > 0;
                
            }
            return false;

        }

        public async Task<bool> DeleteMedical(int id)
        {
            var medical = _context.MedicalRecords.Where(m => m.RecordId == id).FirstOrDefault();
            if(medical != null)
            {
                _context.MedicalRecords.Remove(medical);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
