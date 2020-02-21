using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.PricingCalendarEntries
{
    public class DeleteModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public DeleteModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PricingCalendarEntry PricingCalendarEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PricingCalendarEntry = await _context.PricingCalendarEntries.FirstOrDefaultAsync(m => m.ID == id);

            if (PricingCalendarEntry == null)
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

            PricingCalendarEntry = await _context.PricingCalendarEntries.FindAsync(id);

            if (PricingCalendarEntry != null)
            {
                _context.PricingCalendarEntries.Remove(PricingCalendarEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
