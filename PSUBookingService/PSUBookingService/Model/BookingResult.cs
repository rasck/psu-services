using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSUBookingService.Model
{
    public class BookingResult
    {
        public string ResultCode { get; set; }
        public int BookingId { get; set; }
        public string Message { get; set; }
    }

    public enum BookingResultCode
    {
        HasOverlap = 0,
        Failed,
        Success
    }
}
