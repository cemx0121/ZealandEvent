using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Events
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public EditModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; }

        public Event EventAlreadyExist { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .Include(a => a.Arrangement).FirstOrDefaultAsync(m => m.EventId == id);
            
            if (Event == null)
            {
                return NotFound();
            }
           ViewData["ArrangementId"] = new SelectList(_context.Arrangements, "ArrangementId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;
            Event.Start = DateTime.Parse("2022-01-01 " + Event.Start.TimeOfDay);
            Event.End = DateTime.Parse("2022-01-01 " + Event.End.TimeOfDay);


            EventAlreadyExist = await _context.Events.Where(
            e => e.ArrangementId == Event.ArrangementId &&
            ((e.Start <= Event.Start && e.End >= Event.Start) || (e.End >= Event.End && e.Start <= Event.End)) &&
            (Event.Location == Location.Musikteltet || Event.Location == Location.Tribunen) && (e.Location == Location.Musikteltet || e.Location == Location.Tribunen)
            ).FirstOrDefaultAsync();
            if (EventAlreadyExist != null)
            {
                Message = "Der findes allerede et event i samme tidsramme og lokation til dette arrangement! (Musikteltet & Tribunen kan ikke have et event i samme tidsramme)";
                ViewData["ArrangementId"] = new SelectList(_context.Arrangements, "ArrangementId", "Name");
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

            return RedirectToPage("/Arrangementer/Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
