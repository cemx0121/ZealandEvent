using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Arrangementer
{
    public class DetailsModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public DetailsModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

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
            Events.Sort((x, y) => DateTime.Compare(x.Start, y.Start));
            


            if (Arrangement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
