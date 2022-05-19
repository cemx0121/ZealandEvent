using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }
        public static User LoggedInUser { get; set; } = null;
        private readonly ZealandEventDBContext _context;


        public LoginModel(ZealandEventDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            List<User> users = _context.Users.ToList();
            foreach (User user in users)
            {
                if (UserName == user.UserName && Password == user.Password)
                {

                    LoggedInUser = user;
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName), new Claim(ClaimTypes.Role, user.UserRole.ToString()) };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToPage("/Index");
                }
            }
            Message = "Incorrect username or password";
            return Page();
        }
    }
}
