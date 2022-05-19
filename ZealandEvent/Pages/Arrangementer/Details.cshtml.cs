﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZealandEvent.Services;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Arrangementer
{
    public class DetailsModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public DetailsModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        public Arrangement Arrangement { get; set; }
        public List<Event> Events { get; set; }
        public int AntalPladserTilbage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);
            
            AntalPladserTilbage = 500 - _countService.CountBookings(id);
            
            Events = _countService.FindEventsToArrangement(id);

            Events.Sort((x, y) => DateTime.Compare(x.Start, y.Start));
            


            if (Arrangement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
