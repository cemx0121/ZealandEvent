using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Bookings
{
    [Authorize(Roles = "Guest")]
    public class MyBookingsModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public MyBookingsModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        public List<Booking> Bookings { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            Bookings = await _context.Bookings
    .Include(a => a.Arrangement).Where(b => b.UserId == Login.LoginModel.LoggedInUser.UserId).ToListAsync();
            Bookings.Sort((x, y) => string.Compare(x.Firstname, y.Firstname));

            return Page();
        }
    }
}
