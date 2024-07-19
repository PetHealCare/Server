using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class PaymentDAO
    {
        private static readonly Lazy<PaymentDAO> _instance =
        new Lazy<PaymentDAO>(() => new PaymentDAO(new PetHealthCareContext()));
        public static PaymentDAO Instance => _instance.Value;
        private PetHealthCareContext _context;
        public PaymentDAO(PetHealthCareContext context)
        {
            _context = context;
        }

        public  List<Payment> GetPayments()
        {
            return  _context.Payments.ToList();
        }
        public Payment GetPaymentById(int id)
        {
            return  _context.Payments.Find(id);
        }
        public Payment AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            
            return payment;
        }

        public bool UpdatePayment(Payment payment)
        {
            var paymentUpdate = _context.Payments.Find(payment.PaymentId);
            paymentUpdate.Amount = payment.Amount;
            paymentUpdate.Method = payment.Method;
            paymentUpdate.InsDate = payment.InsDate;
            paymentUpdate.Status = payment.Status;
            paymentUpdate.BillId = payment.BillId;
            return _context.SaveChanges() > 0;
        }
        public bool DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            _context.Payments.Remove(payment);
            return _context.SaveChanges() > 0;
        }
    }
}
