﻿using System;
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
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public IndexModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        public IList<Arrangement> Arrangement { get;set; }

        public async Task OnGetAsync()
        {
            Arrangement = await _context.Arrangements.ToListAsync();
        }
    }
}
