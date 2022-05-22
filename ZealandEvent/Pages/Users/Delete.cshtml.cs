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

namespace ZealandEvent.Pages.Users
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
        public User User { get; set; }

        public List<Booking> Bookings { get; set; }

        public int AntalBookinger { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            Bookings = _countService.FindBookingsToUser(id);
            AntalBookinger = Bookings.Count();

            if (User == null)
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

            User = await _context.Users.FindAsync(id);
            Bookings = _countService.FindBookingsToUser(id);

            if (User != null)
            {
                foreach (var b in Bookings)
                {
                    _context.Bookings.Remove(b);
                }
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
