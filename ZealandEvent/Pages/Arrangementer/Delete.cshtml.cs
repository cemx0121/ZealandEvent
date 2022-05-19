using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZealandEvent.Services;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Arrangementer
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public DeleteModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        [BindProperty]
        public Arrangement Arrangement { get; set; }

        public List<Event> Events { get; set; }

        public List<Booking> Bookings { get; set; }
        public int AntalTilmeldte { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);
            Events = _countService.FindEventsToArrangement(id);
            Bookings = _countService.FindBookingsToArrangement(id);
            AntalTilmeldte = Bookings.Count();

            if (Arrangement == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Arrangement = await _context.Arrangements.FindAsync(id);
            Events = _countService.FindEventsToArrangement(id);
            Bookings = _countService.FindBookingsToArrangement(id);

            if (Arrangement != null)
            {
                foreach (var e in Events)
                {
                    _context.Events.Remove(e);
                }
                foreach (var b in Bookings)
                {
                    _context.Bookings.Remove(b);
                }
                _context.Arrangements.Remove(Arrangement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
