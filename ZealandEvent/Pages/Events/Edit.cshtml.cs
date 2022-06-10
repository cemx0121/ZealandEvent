using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZealandEvent.Services;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Events
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public EditModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        [BindProperty]
        public Event Event { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? eventId)
        {
            if (eventId == null)
            {
                return NotFound();
            }
            
            Event = await _context.Events
                .Include(a => a.Arrangement).FirstOrDefaultAsync(m => m.EventId == eventId);
            
            if (Event == null)
            {
                return NotFound();
            }
           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int arrangementId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            

            _context.Attach(Event).State = EntityState.Modified;
            Event.ArrangementId = arrangementId;
            Event.Start = DateTime.Parse("2022-01-01 " + Event.Start.TimeOfDay);
            Event.End = DateTime.Parse("2022-01-01 " + Event.End.TimeOfDay);

            if (_countService.CheckForDuplicateEvent(Event) != null && _countService.CheckForDuplicateEvent(Event).EventId != Event.EventId)
            {
                Message = "Der findes allerede et event i samme tidsramme og lokation til dette arrangement! (Musikteltet & Tribunen kan ikke have et event i samme tidsramme)";
                return Page();
            }
            else if (Event.Start > Event.End)
            {
                Message = "Start tidspunktet på et event kan ikke sættes til efter slut tidspunktet";
                return Page();
            }
            else
            {
             
              try
              {
                 
                 await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                 if (!EventExists(Event.EventId))
                 {
                    return NotFound();
                 }
                 else
                 {
                    throw;
                 }
              }
            }

            return RedirectToPage("/Arrangementer/Details", new { id = arrangementId.ToString() });
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
