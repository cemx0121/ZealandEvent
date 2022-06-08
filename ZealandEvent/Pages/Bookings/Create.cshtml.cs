using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZealandEvent.Services;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace ZealandEvent.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly ZealandEventDBContext _context;
        private readonly ICountService _countService;

        public CreateModel(ZealandEventDBContext context, ICountService countService)
        {
            _context = context;
            _countService = countService;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);

            AntalPPladserTilbage = 90 - _countService.CountParkings(id);
            AntalPPladserTilbageIProcent = (AntalPPladserTilbage / 90) * 100;
            AntalPPladserTilbageIProcentTilInt = Convert.ToInt32(AntalPPladserTilbageIProcent);

            return Page();
        }


        [BindProperty]
        public Booking Booking { get; set; }
        public Arrangement Arrangement { get; set; }
        public string Message { get; set; }
        public double AntalPPladserTilbage { get; set; }
        public double AntalPPladserTilbageIProcent { get; set; }
        public int AntalPPladserTilbageIProcentTilInt { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Arrangement = await _context.Arrangements.FirstOrDefaultAsync(m => m.ArrangementId == id);
            Booking.ArrangementId = Arrangement.ArrangementId;

            if (_countService.CountBookings(id) >= 500)
            {
                Message = "Dette arrangement er desværre fuldt booket!";
                AntalPPladserTilbage = 90 - _countService.CountParkings(id);
                AntalPPladserTilbageIProcent = (AntalPPladserTilbage / 90) * 100;
                AntalPPladserTilbageIProcentTilInt = Convert.ToInt32(AntalPPladserTilbageIProcent);
                return Page();
            }
            else if (_countService.CountParkings(id) >= 90 && Booking.Parking == true)
            {
                Message = "Dette arrangements p-pladser er desværre fuldt booket";
                AntalPPladserTilbage = 90 - _countService.CountParkings(id);
                AntalPPladserTilbageIProcent = (AntalPPladserTilbage / 90) * 100;
                AntalPPladserTilbageIProcentTilInt = Convert.ToInt32(AntalPPladserTilbageIProcent);
                return Page();
            }
            else
            {
                Booking.UserId = Login.LoginModel.LoggedInUser.UserId;
                _context.Bookings.Add(Booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./BookingConfirmation");
        }
    }
}
