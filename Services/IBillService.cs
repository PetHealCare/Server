using BusinessObjects.Models;
using DTOs.Request.BillRequest;
using DTOs.Response.Bill;
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
        public List<BillResponse> GetBills();
        public BillResponse GetBillById(int id);
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
                InsDate = bill.InsDate
            };
            return _billRepository.AddBill(request);
        }

        public bool DeleteBill(int id)
        {
            return _billRepository.DeleteBill(id);
        }

        public BillResponse GetBillById(int id)
        {
            var bill = _billRepository.GetBillById(id);
            if (bill == null)
            {
                return null;
            }
            return new BillResponse
            {
                BillId = bill.BillId,
                BookingId = bill.BookingId,
                TotalAmount = bill.TotalAmount,
                InsDate = bill.InsDate
            };
            
        }

        public List<BillResponse> GetBills()
        {
            var bills = _billRepository.GetBills();
            return bills.Select(bill => new BillResponse
            {
                BillId = bill.BillId,
                BookingId = bill.BookingId,
                TotalAmount = bill.TotalAmount,
                InsDate = bill.InsDate
            }).ToList();
            
        }

        public bool UpdateBill(BillRequest bill)
        {
            var request = new Bill
            {
                BillId = bill.BillId,
                BookingId = bill.BookingId,
                TotalAmount = bill.TotalAmount,
                InsDate = bill.InsDate
            };
            return _billRepository.UpdateBill(request);
        }
    }
}
