using BusinessObjects.Models;
using DTOs.Request.Customer;
using DTOs.Request.Staff;
using DTOs.Response.Staff;
using Repositories;
using Services.Extentions.Paginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IStaffService
    {
		Task<StaffResponse> Create(CreateStaffRequest request);
		Task<StaffResponse> Update(UpdateStaffRequest request);
		Task<StaffResponse> GetById(int id);
		Task<StaffResponse> GetByUserId(int userId);
		Task<PaginatedList<StaffResponse>> GetList(GetListStaffRequest request);
		Task<bool> Delete(int id);
	}
}
