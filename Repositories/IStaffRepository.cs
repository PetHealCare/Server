﻿using BusinessObjects.Models;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IStaffRepository
    {
        public List<staff> GetAll();
        public staff Get(int id);
        public staff Create(staff staff);
        public bool Update(staff staff);
        public bool Delete(int id);
        public staff Login(string username, string password);
    }
    public class StaffRepository : IStaffRepository
    {
        public staff Create(staff staff)
        {
            return StaffDAO.Instance.Create(staff);
        }

        public bool Delete(int id)
        {
            return StaffDAO.Instance.Delete(id);
        }

        public staff Get(int id)
        {
            return StaffDAO.Instance.GetById(id);
        }

        public List<staff> GetAll()
        {
            return StaffDAO.Instance.GetAll();
        }

        public staff Login(string username, string password)
        {
            return StaffDAO.Instance.Login(username, password);
        }

        public bool Update(staff staff)
        {
           return StaffDAO.Instance.Update(staff);
        }
    }
}
