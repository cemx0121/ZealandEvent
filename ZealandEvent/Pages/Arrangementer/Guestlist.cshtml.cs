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
    public class GuestlistModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public GuestlistModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        public Arrangement Arrangement { get; set; }
        public List<Booking> Bookings { get; set; }
        public double AntalTilmeldte { get; set; }
        public double AntalTilmeldteIProcent { get; set; }
        public int AntalTilmeldteIProcentTilInt { get; set; }
        [BindProperty]
        public string SearchText { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);

            Bookings = _countService.FindBookingsToArrangement(id);
            Bookings.Sort((x, y) => string.Compare(x.Firstname, y.Firstname));
            
            AntalTilmeldte = Bookings.Count();
            AntalTilmeldteIProcent = (AntalTilmeldte / 500) * 100;
            AntalTilmeldteIProcentTilInt = Convert.ToInt32(AntalTilmeldteIProcent);


            if (Arrangement == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);

            Bookings = _countService.FindBookingsToArrangement(id);
            Bookings.Sort((x, y) => string.Compare(x.Firstname, y.Firstname));
            
            AntalTilmeldte = Bookings.Count();
            AntalTilmeldteIProcent = (AntalTilmeldte / 500) * 100;
            AntalTilmeldteIProcentTilInt = Convert.ToInt32(AntalTilmeldteIProcent);

            Bookings = _countService.SearchForBookersName(SearchText, id);
            return Page();
        }
    }
}
