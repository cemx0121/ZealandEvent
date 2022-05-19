using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Services
{
    public class CountService:ICountService
    {
        private readonly ZealandEventDBContext _context;

        public CountService(ZealandEventDBContext context)
        {
            _context = context;
        }


        public int CountBookings(int? id)
        {
            Task<List<Booking>> AntalBookinger;
            AntalBookinger = _context.Bookings
            .Include(a => a.Arrangement).Where(b => b.ArrangementId == id).ToListAsync();
            return AntalBookinger.Result.Count();
        }

        public int CountParkings(int? id)
        {
            Task<List<Booking>> AntalParkeringer;
            AntalParkeringer = _context.Bookings
            .Include(a => a.Arrangement).Where(b => b.Parking == true && b.ArrangementId == id).ToListAsync();
            return AntalParkeringer.Result.Count();
        }

        public Event CheckForDuplicateEvent(Event newEvent)
        {
            Task<Event> DuplicateEvent;
            DuplicateEvent = _context.Events.Where(
            e => e.ArrangementId == newEvent.ArrangementId &&
            ((e.Start <= newEvent.Start && e.End >= newEvent.Start) || (e.End >= newEvent.End && e.Start <= newEvent.End)) &&
            (newEvent.Location == Location.Musikteltet || newEvent.Location == Location.Tribunen) && (e.Location == Location.Musikteltet || e.Location == Location.Tribunen)
            ).FirstOrDefaultAsync();
            return DuplicateEvent.Result;
        }

        public List<Event> FindEventsToArrangement(int? id)
        {
            Task<List<Event>> EventsToArrangement;
            EventsToArrangement = _context.Events
            .Include(a => a.Arrangement).Where(e => e.ArrangementId == id).ToListAsync();
            return EventsToArrangement.Result;
        }

        public List<Booking> FindBookingsToArrangement(int? id)
        {
            Task<List<Booking>> BookingsToArrangement;
            BookingsToArrangement = _context.Bookings
            .Include(a => a.Arrangement).Where(e => e.ArrangementId == id).ToListAsync();
            return BookingsToArrangement.Result;
        }

        public User CheckDuplicateUsername(User newUser)
        {
            Task<User> DuplicateUser;
            DuplicateUser = _context.Users.Where(u => u.UserName == newUser.UserName).FirstOrDefaultAsync();
            return DuplicateUser.Result;
        }




    }
}
