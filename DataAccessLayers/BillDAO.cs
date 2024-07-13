using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class BillDAO
    {
        private static readonly Lazy<BillDAO> _instance =
        new Lazy<BillDAO>(() => new BillDAO(new PetHealthCareContext()));
        public static BillDAO Instance => _instance.Value;
        private PetHealthCareContext _context;
        public BillDAO(PetHealthCareContext context)
        {
            _context = context;
        }

        public  List<Bill> GetBills()
        {
            return  _context.Bills.ToList();
        }
        public Bill GetBillById(int id)
        {
            return  _context.Bills.Find(id);
        }
        public Bill AddBill(Bill bill)
        {
            _context.Bills.Add(bill);
            _context.SaveChanges();
            
            return bill;
        }

        public bool UpdateBill(Bill bill)
        {
            var billUpdate = _context.Bills.Find(bill.BillId);
            billUpdate.BookingId = bill.BookingId;
            billUpdate.TotalAmount = bill.TotalAmount;
            billUpdate.PaymentId = bill.PaymentId;
            billUpdate.InsDate = bill.InsDate;
            _context.SaveChanges();
            return _context.SaveChanges() > 0;
        }
        public bool DeleteBill(int id)
        {
            var bill = _context.Bills.Find(id);
            _context.Bills.Remove(bill);
            return _context.SaveChanges() > 0;
        }

        
        
    }
}
