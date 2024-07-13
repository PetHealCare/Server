using BusinessObjects.Models;
using DTOs.Request.Payment;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentService
    {
        List<Payment> GetPayments();
        Payment GetPaymentById(int id);
        Payment AddPayment(PaymentRequest payment);
        bool UpdatePayment(PaymentRequest payment);
        bool DeletePayment(int id);
    }
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        

        public List<Payment> GetPayments()
        {
            return _paymentRepository.GetPayments();
        }
        public Payment GetPaymentById(int id)
        {
            return _paymentRepository.GetPaymentById(id);
        }
        public Payment AddPayment(PaymentRequest payment)
        {
            var request = new Payment
            {
                Amount = payment.Amount,
                Method = payment.Method,
                InsDate = payment.InsDate,
                Status = payment.Status
            };
            return _paymentRepository.AddPayment(request);

            
        }

        public bool UpdatePayment(PaymentRequest payment)
        {
            var request = new Payment
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                Method = payment.Method,
                InsDate = payment.InsDate,
                Status = payment.Status
            };
            return _paymentRepository.UpdatePayment(request);
        }
        public bool DeletePayment(int id)
        {
            var payment = _paymentRepository.GetPaymentById(id);
            if (payment == null)
            {
                return false;
            }
            return _paymentRepository.DeletePayment(id);
        }
    }
}
