using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Arrangementer
{
    [Authorize(Roles = "Admin")]
    public class GuestlistModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public GuestlistModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        public Arrangement Arrangement { get; set; }
        public List<Booking> Bookings { get; set; }
        public int AntalTilmeldte { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);

            Bookings = await _context.Bookings
    .Include(a => a.Arrangement).Where(b => b.ArrangementId == id).ToListAsync();
            AntalTilmeldte = Bookings.Count();



            if (Arrangement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
