﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ZealandEventLib.Data.ZealandEventDBContext _context;

        public IndexModel(ZealandEventLib.Data.ZealandEventDBContext context)
        {
            _context = context;
        }

        public List<Arrangement> Arrangement { get; set; }
        

        public async Task OnGetAsync()
        {
            Arrangement = await _context.Arrangements.ToListAsync();
            Arrangement.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

        }
    }
}
