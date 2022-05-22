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

        #region Searching Methods
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

        public List<Booking> FindBookingsToUser(int? id)
        {
            Task<List<Booking>> BookingsToUser;
            BookingsToUser = _context.Bookings
            .Include(a => a.Arrangement).Where(b => b.UserId == id).ToListAsync();
            return BookingsToUser.Result;
        }

        public List<Arrangement> SearchForArrangement(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Task<List<Arrangement>> AllArrangements;
                AllArrangements = _context.Arrangements.ToListAsync();
                return AllArrangements.Result;
            }
            else
            {
                Task<List<Arrangement>> SpecificArrangements;
                SpecificArrangements = _context.Arrangements.Where(a => a.Name.Contains(searchText)).ToListAsync();
                return SpecificArrangements.Result;
            }
        }

        public List<Booking> SearchForBookersName(string searchText, int? id)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Task<List<Booking>> AllBookings;
                AllBookings = _context.Bookings.Where(b => b.ArrangementId == id).ToListAsync();
                return AllBookings.Result;
            }
            else
            {
                Task<List<Booking>> SpecificBookings;
                SpecificBookings = _context.Bookings.Where(b => b.Firstname.Contains(searchText) || b.Lastname.Contains(searchText)).Where(b => b.ArrangementId == id).ToListAsync();
                return SpecificBookings.Result;
            }
        }

        public List<User> SearchForUserName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Task<List<User>> AllUsers;
                AllUsers = _context.Users.ToListAsync();
                return AllUsers.Result;
            }
            else
            {
                Task<List<User>> SpecificUsers;
                SpecificUsers = _context.Users.Where(u => u.UserName.Contains(searchText)).ToListAsync();
                return SpecificUsers.Result;
            }
        }

        #endregion

        #region Counting Methods
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

        #endregion

        #region Checking Methods
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

        public User CheckDuplicateUsername(User newUser)
        {
            Task<User> DuplicateUser;
            DuplicateUser = _context.Users.Where(u => u.UserName == newUser.UserName).FirstOrDefaultAsync();
            return DuplicateUser.Result;
        }

        #endregion

    }
}
