using BusinessObjects.Models;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetPayments();
        Payment GetPaymentById(int id);
        Payment AddPayment(Payment payment);
        bool UpdatePayment(Payment payment);
        bool DeletePayment(int id);
    }
    public class PaymentRepository : IPaymentRepository
    {
        

        public List<Payment> GetPayments()
        {
            return PaymentDAO.Instance.GetPayments();
        }
        public Payment GetPaymentById(int id)
        {
            return PaymentDAO.Instance.GetPaymentById(id);
        }
        public Payment AddPayment(Payment payment)
        {
            return PaymentDAO.Instance.AddPayment(payment);

            
        }

        public bool UpdatePayment(Payment payment)
        {
            return PaymentDAO.Instance.UpdatePayment(payment);
        }
        public bool DeletePayment(int id)
        {
            return PaymentDAO.Instance.DeletePayment(id);
        }
    }
}
