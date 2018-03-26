using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSUBookingService.DAL;
using PSUBookingService.Model;

namespace PSUBookingService.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private IBookingRepository bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }
        // GET api/values
        [HttpGet]
        public List<Booking> Get()
        {
            return bookingRepository.LoadAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public List<Booking> Get(string id)
        {
            return bookingRepository.LoadAllFromResource(id);
        }

        // POST api/values
        [HttpPost]
        public BookingResult Post([FromBody]Booking booking)
        {
            BookingResult result = new BookingResult();
            try
            {
                if (!bookingRepository.HasOverLap(booking))
                {
                    bookingRepository.Save(booking);
                    result.BookingId = booking.Id;
                    result.ResultCode = BookingResultCode.Success.ToString();
                }
                else
                {
                    result.ResultCode = BookingResultCode.HasOverlap.ToString();
                    result.Message = "This booking conflicts with another booking of this resource";
                }
                return result;                
            }
            catch(Exception ex)
            {
                result.ResultCode = BookingResultCode.Failed.ToString();
                result.Message = ex.Message;
            }
            return result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public BookingResult Delete(int id)
        {
            BookingResult result = new BookingResult();
            result.BookingId = id;
            try
            {
                bookingRepository.Delete(id);                
                result.ResultCode = BookingResultCode.Success.ToString();
            }
            catch(Exception ex)
            {                
                result.ResultCode = BookingResultCode.Failed.ToString(); ;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
