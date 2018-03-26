using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSUPaymentService.Model
{
    public class Payment
    {
        /// <summary>
        /// This is the credit card number
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// This is the expiry month for the credit card
        /// </summary>
        public int ExpiryMonth { get; set; }
        /// <summary>
        /// This is the expiry year for the credit card
        /// </summary>
        public int ExpiryYear { get; set; }
        /// <summary>
        /// This is the security code for the payment, this is found on the back of a credit card
        /// </summary>
        public int SecurityCode { get; set; }
        /// <summary>
        /// This is the order id. This Id is internal to the calling application
        /// </summary>
        public int OrderId { get; set; }
    }
}
