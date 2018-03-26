using PSUBookingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSUBookingService.DAL
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        public BookingRepository(DataContext context) : base(context) { }

        public void Delete(int id)
        {
            Booking existingBooking = Load(id);
            if (existingBooking != null)
            {
                context.Bookings.Remove(existingBooking);
            }
        }

        public bool HasOverLap(Booking booking)
        {
            //has overlap if
            //1. either start or end is between start and end of existing
            //2. start and end is outside of existing
            //3. start and end is between start and end of existing
            var query = from b in context.Bookings
                        join c in context.Bookings
                        on b.Id equals c.Id
                        where
                        (b.ResourceId == booking.ResourceId) 
                        &&
                        ((booking.StartTime >= b.StartTime && booking.StartTime <= b.EndTime)
                        ||
                        (booking.EndTime >= b.StartTime && booking.EndTime <= b.EndTime)
                        ||
                        (booking.StartTime >= b.StartTime && booking.EndTime <= b.EndTime)
                        ||
                        (booking.StartTime <= b.StartTime && booking.EndTime >= b.EndTime))
                        select b;
            return query.Any();                                   
        }

        public Booking Load(int id)
        {
            return context.Bookings.FirstOrDefault(x => x.Id == id);
        }

        public List<Booking> LoadAll()
        {
            return context.Bookings.ToList();
        }

        public List<Booking> LoadAllFromResource(string id)
        {
            return context.Bookings.Where(x => x.ResourceId == id).ToList();
        }

        public void Save(Booking booking)
        {
            context.Bookings.Add(booking);
            context.SaveChanges();
        }
    }
}
