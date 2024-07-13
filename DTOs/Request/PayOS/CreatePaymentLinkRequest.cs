using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Request.PayOS
{
    public class CreatePaymentLinkRequest
    {
        
            public int PaymentId { get; set; }
            public string returnUrl { get; set; }
            public string cancelUrl { get; set; }

        
    }
}
