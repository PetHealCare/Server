using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class StaffDAO : GenericDAO<staff>
    {
        private static readonly Lazy<StaffDAO> _instance =
        new Lazy<StaffDAO>(() => new StaffDAO(new PetHealthCareContext()));
        public static StaffDAO Instance => _instance.Value;
        public StaffDAO(PetHealthCareContext context) : base(context)
        {

        }
       
        public staff Create(staff staff)
        {
            _context.Add(staff);
            _context.SaveChanges();
            return staff;
        }
        public bool Update(staff staff)
        {
            var staffUpdate = GetById(staff.StaffId);
            if (staffUpdate == null)
            {
                return false;
            }
            staffUpdate.FullName = staff.FullName;
            staffUpdate.PhoneNumber = staff.PhoneNumber;
           
          
            _context.staff.Update(staffUpdate);
            return _context.SaveChanges() > 0;
        }
       
    }
}
