using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.PricingCalendarEntryWindows
{
    public class EditModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public EditModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RentalPeriodPricingCalendarWindow PricingCalendarEntryWindow { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PricingCalendarEntryWindow = await _context.RentalPeriodPricingCalendarWindows.FirstOrDefaultAsync(m => m.ID == id);

            if (PricingCalendarEntryWindow == null)
            {
                return NotFound();
            }
           ViewData["PricingCalendarEntryID"] = new SelectList(_context.PricingCalendarEntries, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PricingCalendarEntryWindow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricingCalendarEntryWindowExists(PricingCalendarEntryWindow.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PricingCalendarEntryWindowExists(int id)
        {
            return _context.RentalPeriodPricingCalendarWindows.Any(e => e.ID == id);
        }
    }
}
