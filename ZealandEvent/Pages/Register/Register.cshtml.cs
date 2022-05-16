using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        [BindProperty]
        public User User { get; set; }



        public RegisterModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;

        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            User.UserRole = UserRole.Guest;
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Login/Login");
        }
    }
}
