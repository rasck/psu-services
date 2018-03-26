using PSUBookingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSUBookingService.DAL
{
    public interface IBookingRepository
    {
        void Save(Booking booking);
        Booking Load(int id);
        List<Booking> LoadAll();
        void Delete(int id);
        List<Booking> LoadAllFromResource(string id);
        bool HasOverLap(Booking booking);
    }
}
