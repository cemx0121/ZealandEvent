using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandEvent.Services;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        [BindProperty]
        public User User { get; set; }

        public string Message { get; set; }



        public RegisterModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;

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

            if (_countService.CheckDuplicateUsername(User) != null)
            {
                Message = "Der findes allerede en bruger med det brugernavn. Vælg et andet brugernavn.";
                return Page();
            }
            else
            {
                _context.Users.Add(User);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Users/UserConfirmation");
        }
    }
}
