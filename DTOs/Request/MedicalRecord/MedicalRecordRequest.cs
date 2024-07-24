using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.MedicalRecord
{
    public class MedicalRecordRequest
    {
        public int PetId { get; set; }
        public int DoctorId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Notes { get; set; }
    }
}
