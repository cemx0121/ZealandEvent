using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandEventLib.Models;

namespace ZealandEvent.Services
{
    public class CountService:ICountService
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public CountService(ZealandEventLib.Data.ZealandEventDBContext context)
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


    }
}
