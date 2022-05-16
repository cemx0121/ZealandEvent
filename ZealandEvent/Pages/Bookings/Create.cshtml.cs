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
        public List<Booking> Bookings { get; set; }
        public string Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Bookings = await _context.Bookings
    .Include(a => a.Arrangement).Where(b => b.ArrangementId == id).ToListAsync();
            int noOfBookings = Bookings.Count();
            if (noOfBookings < 500)
            {
                _context.Bookings.Add(Booking);
                await _context.SaveChangesAsync();
            }
            else
            {
                Message = "Dette arrangement er fuldt booket.";
                return Page();
            }

            return RedirectToPage("./BookingConfirmation");
        }
    }
}
