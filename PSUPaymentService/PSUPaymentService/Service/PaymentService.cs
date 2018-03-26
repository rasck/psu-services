using PSUPaymentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSUPaymentService.Service
{
    public class PaymentService
    {
        public PaymentResult ProcessPayment(Payment payment)
        {
            PaymentResult result = new PaymentResult();
            result.TransactionId = Guid.NewGuid().ToString();
            result.ResultCode = PaymentCode.Success.ToString();
            result.OrderId = payment.OrderId;
            //random errors
            Random r = new Random();
            double d = r.NextDouble();
            if(d > 0.80)
            {
                //10% of the time the system fails
                result.ResultCode = PaymentCode.Unknown.ToString();
                result.TransactionId = null;
                result.ResultMessage = DateTime.Now.ToString() + ": Unknown error happened in the payment service. Tough luck. Try again.";
            }
            //simulate payment, 1 second delay
            System.Threading.Thread.Sleep(1000);
            return result;
        }
    }
}
