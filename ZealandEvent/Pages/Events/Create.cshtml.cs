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
    public class CreateModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public CreateModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }
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
            Event.Start = DateTime.Parse("2022-01-01 " + Event.Start.TimeOfDay);
            Event.End = DateTime.Parse("2022-01-01 " + Event.End.TimeOfDay);

            if (_countService.CheckForDuplicateEvent(Event) != null)
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

                _context.Events.Add(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Arrangementer/Details", new { id = id.ToString() });
        }
    }
}
