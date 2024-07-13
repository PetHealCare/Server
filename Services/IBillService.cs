using BusinessObjects.Models;
using DTOs.Request.BillRequest;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBillService
    {
        public List<Bill> GetBills();
        public Bill GetBillById(int id);
        public Bill AddBill(BillRequest bill);
        public bool UpdateBill(BillRequest bill);
        public bool DeleteBill(int id);
    }
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public Bill AddBill(BillRequest bill)
        {
            var request = new Bill
            {
                BookingId = bill.BookingId,
                TotalAmount = bill.TotalAmount,
                PaymentId = bill.PaymentId,
                InsDate = bill.InsDate
            };
            return _billRepository.AddBill(request);
        }

        public bool DeleteBill(int id)
        {
            return _billRepository.DeleteBill(id);
        }

        public Bill GetBillById(int id)
        {
            return _billRepository.GetBillById(id);
        }

        public List<Bill> GetBills()
        {
            return _billRepository.GetBills();
        }

        public bool UpdateBill(BillRequest bill)
        {
            var request = new Bill
            {
                BillId = bill.BillId,
                BookingId = bill.BookingId,
                TotalAmount = bill.TotalAmount,
                PaymentId = bill.PaymentId,
                InsDate = bill.InsDate
            };
            return _billRepository.UpdateBill(request);
        }
    }
}
