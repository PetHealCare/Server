using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.Request.Doctor;
using DTOs.Response.Doctor;
using Services.Extentions.Paginate;

namespace Services.Interface
{
	public interface IDoctorService
	{
		Task<DoctorResponse> Create(CreateDoctorRequest request);
		Task<DoctorResponse> Update(UpdateDoctorRequest request);
		Task<DoctorResponse> GetById(int id);
		Task<DoctorResponse> GetByUserId(int userId);
		Task<PaginatedList<DoctorResponse>> GetList(GetListDoctorRequest request);
		Task<bool> Delete(int id);
	}
}
