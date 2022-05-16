using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public CreateModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ArrangementId"] = new SelectList(_context.Arrangements, "ArrangementId", "Name");
            return Page();
        }

      


        [BindProperty]
        public Booking Booking { get; set; }
        public List<Booking> AntalBookinger { get; set; }
        public List<Booking> AntalParkeringer { get; set; }
        public string Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            /// Tæller hvor mange bookinger der er til arrangementet, tæller dem og tildeler dem til en int
            AntalBookinger = await _context.Bookings
    .Include(a => a.Arrangement).Where(b => b.ArrangementId == id).ToListAsync();
            int noOfBookings = AntalBookinger.Count();

            /// Tæller hvor mange bookinger der har takket ja til parkering, tæller dem og tildeler dem til en int
            AntalParkeringer = await _context.Bookings
    .Include(a => a.Arrangement).Where(b => b.Parking == true).ToListAsync();
            int noOfParkings = AntalParkeringer.Count();

            if (noOfBookings > 500)
            {
                Message = "Dette arrangement er desværre fuldt booket!";
                return Page();
            }
            else if (noOfParkings > 90)
            {
                Message = "Dette arrangements p-pladser er desværre fuldt booket";
                return Page();
            }
            else
            {
                Booking.UserId = Login.LoginModel.LoggedInUser.UserId;
                _context.Bookings.Add(Booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./BookingConfirmation");
        }
    }
}
