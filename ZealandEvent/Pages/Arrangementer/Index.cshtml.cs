using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Arrangementer
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ZealandEventDBContext _context;

        public IndexModel(ZealandEventDBContext context)
        {
            _context = context;
        }

        public List<Arrangement> Arrangement { get;set; }

        public async Task OnGetAsync()
        {
            Arrangement = await _context.Arrangements.ToListAsync();
            Arrangement.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
        }
    }
}
