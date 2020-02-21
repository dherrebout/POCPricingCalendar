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
    public class DeleteModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public DeleteModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RentalPeriod = await _context.RentalPeriods.FindAsync(id);

            if (RentalPeriod != null)
            {
                _context.RentalPeriods.Remove(RentalPeriod);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
