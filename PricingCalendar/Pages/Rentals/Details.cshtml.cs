using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.Rentals
{
    public class DetailsModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public DetailsModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        public Rental Rental { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rental = await _context.Rentals.FirstOrDefaultAsync(m => m.ID == id);

            if (Rental == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
