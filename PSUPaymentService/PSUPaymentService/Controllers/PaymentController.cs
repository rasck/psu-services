using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSUPaymentService.Model;
using System.Net.Http;
using PSUPaymentService.Service;

namespace PSUPaymentService.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {        
        //This method is always returning http 200. But the content of the service will determine what went wrong.
        [HttpPost]
        public PaymentResult Post([FromBody]Payment payment)
        {
            //var payment = obj as Payment;
            //guard clauses
            if (string.IsNullOrEmpty(payment.CardNo))
            {
                return new PaymentResult() { ResultCode = PaymentCode.FailedCardNo.ToString(), ResultMessage = DateTime.Now.ToString() + ": Card Number is required" };
            }
            else if(payment.ExpiryMonth <1 || payment.ExpiryMonth > 12)
            {
                return new PaymentResult() { ResultCode = PaymentCode.FailedMonth.ToString(), ResultMessage = DateTime.Now.ToString() + ": Illegal expiry month" };
            }
            else if(payment.ExpiryYear < DateTime.Now.Year || payment.ExpiryYear > DateTime.Now.Year + 5)
            {
                return new PaymentResult() { ResultCode = PaymentCode.FailedYear.ToString(), ResultMessage = DateTime.Now.ToString() + ": Illegal expiry year (must be between now and + 5 years)" };
            }
            else if(payment.SecurityCode < 100 || payment.SecurityCode > 999)
            {
                return new PaymentResult() { ResultCode = PaymentCode.FailedSecurityCode.ToString(), ResultMessage = DateTime.Now.ToString() + ": Illegal Security code. Must be 3 digits" };
            }
            //process the payment
            PaymentService paymentService = new PaymentService();
            return paymentService.ProcessPayment(payment);
        }        
    }
}
