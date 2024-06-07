﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Repositories.Interface
{
	public interface IDoctorRepository
	{
		public Task<Doctor> Create(Doctor request);
		public Task<Doctor> Update(Doctor request);
		public Task<Doctor> GetDoctorById(int id);
		public Task<IList<Doctor>> GetList();
	}
}
