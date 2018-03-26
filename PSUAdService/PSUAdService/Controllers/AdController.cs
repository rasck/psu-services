using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSUAdService.Model;
using PSUAdService.Utilities;

namespace PSUAdService.Controllers
{
    [Route("api/[controller]")]
    public class AdController : Controller
    {
        // GET api/values
        [HttpGet]
        public Ad Get()
        {
            Ad ad = CreateUnstableAdResponse();            
            return ad;
        }

        //simulates various delays, timeouts, exceptions and errors
        //5% of the calls will throw an exception
        //10% of the calls will time out
        //20% of the calls will just have an error
        //30 % of the calls will have varying response times (0.5 - 3 secs)
        private Ad CreateUnstableAdResponse()
        {
            Ad ad = new Ad();
            ad.ImageData = AdGenerator.GenerateAd();
            //randomize it
            Random r = new Random();
            double d = r.NextDouble();
            if(d >= 0.95)
            {
                //5%, throw an exception
                throw new Exception("Darn - a problem occurred on the server. Bad implementation I guess :-)");
            }
            else if (d >= 0.9)
            {
                //10%, time out
                
                System.Threading.Thread.Sleep(30000); //sleep 30 seconds
            }
            else if(d >= 0.8)
            {
                //20% - add an error
                ad.ImageData = "UPS!!! AN ERROR OCCURRED. TIME: " + DateTime.Now.ToString();
            }
            else if(d>= 0.3)
            {
                //30% - vary the response time
                int msToSleep = r.Next(100, 3000);
                System.Threading.Thread.Sleep(msToSleep);
            }
            else
            {
                //the rest - do nothing
            }
            return ad;
        }
       
    }
}
