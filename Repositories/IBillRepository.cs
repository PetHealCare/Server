using BusinessObjects.Models;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBillRepository
    {
        List<Bill> GetBills();
        Bill GetBillById(int id);
        Bill AddBill(Bill bill);
        bool UpdateBill(Bill bill);
        bool DeleteBill(int id);
    }
    public class BillRepository : IBillRepository
    {
       

        public List<Bill> GetBills()
        {
            return BillDAO.Instance.GetBills();
        }
        public Bill GetBillById(int id)
        {
            return BillDAO.Instance.GetBillById(id);
        }
        public Bill AddBill(Bill bill)
        {
            return BillDAO.Instance.AddBill(bill);
        }

        public bool UpdateBill(Bill bill)
        {
            return BillDAO.Instance.UpdateBill(bill);
        }
        public bool DeleteBill(int id)
        {
            return BillDAO.Instance.DeleteBill(id);
        }
    }
}
