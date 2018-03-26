using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSUBookingService.DAL
{
    public class BaseRepository
    {
        protected readonly DataContext context;
        public BaseRepository(DataContext context)
        {
            this.context = context;
        }
    }
}
