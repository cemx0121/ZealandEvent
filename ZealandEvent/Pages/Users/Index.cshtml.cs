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
    public class IndexModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public IndexModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        public List<User> User { get;set; }
        [BindProperty]
        public string SearchText { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
            User.Sort((x, y) => string.Compare(x.UserName, y.UserName));
        }

        public IActionResult OnPost()
        {
            User = _countService.SearchForUserName(SearchText);
            User.Sort((x, y) => string.Compare(x.UserName, y.UserName));
            return Page();
        }
    }
}
