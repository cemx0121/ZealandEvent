using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Arrangementer
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public DeleteModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Arrangement Arrangement { get; set; }

        public List<Event> Events { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);
            Events = await _context.Events
   .Include(a => a.Arrangement).Where(e => e.ArrangementId == id).ToListAsync();

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
            Events = await _context.Events
   .Include(a => a.Arrangement).Where(e => e.ArrangementId == id).ToListAsync();

            if (Arrangement != null)
            {
                foreach (var e in Events)
                {
                    _context.Events.Remove(e);
                }
                _context.Arrangements.Remove(Arrangement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
