using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandEvent.Services;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public IndexModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        public List<Arrangement> Arrangement { get; set; }
        [BindProperty]
        public string SearchText { get; set; }


        public async Task OnGetAsync()
        {
            Arrangement = await _context.Arrangements.ToListAsync();
            Arrangement.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

        }

        public IActionResult OnPost()
        {
            Arrangement = _countService.SearchForArrangement(SearchText);
            Arrangement.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return Page();
        }
    }
}
