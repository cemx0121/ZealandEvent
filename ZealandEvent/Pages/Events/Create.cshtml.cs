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
    public class CreateModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public CreateModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }
        public Event EventAlreadyExist { get; set; }
        public Arrangement Arrangement { get; set; }
        public string Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);
            Event.ArrangementId = Arrangement.ArrangementId;
            
            // IKKE FÆRDIGT
            EventAlreadyExist = await _context.Events.Where(e => 
            e.ArrangementId == Event.ArrangementId && 
            ((e.Start <= Event.Start && e.End >= Event.Start) || (e.End >= Event.End && e.Start <= Event.End))

            ).FirstOrDefaultAsync();
         

            if (EventAlreadyExist != null)
            {
                Message = "Der findes allerede et event i samme tidsrum & lokation til dette arrangement!";
                return Page();
            }
            else
            {

                _context.Events.Add(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Arrangementer/Index");
        }
    }
}
