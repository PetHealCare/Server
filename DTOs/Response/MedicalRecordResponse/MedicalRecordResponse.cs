using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response.MedicalRecordResponse
{
    public class MedicalRecordResponse
    {
        public int RecordId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int DoctorId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Notes { get; set; }
    }
}
