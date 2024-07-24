using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.MedicalRecord
{
    public class GetListMedicalRecordRequest
    {
        public int PetId { get; set; }
        public int DoctorId { get; set; }
    }
}
