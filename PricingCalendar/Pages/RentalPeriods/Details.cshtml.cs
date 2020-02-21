using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.RentalPeriods
{
    public class DetailsModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public DetailsModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        public RentalPeriod RentalPeriod { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RentalPeriod = await _context.RentalPeriods
                .Include(r => r.Rental).FirstOrDefaultAsync(m => m.ID == id);

            if (RentalPeriod == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
