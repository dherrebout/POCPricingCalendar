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

namespace PricingCalendar.Pages.PricingCalendarEntries
{
    public class EditModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public EditModel(PricingCalendar.Data.PricingCalendarContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PricingCalendarEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricingCalendarEntryExists(PricingCalendarEntry.ID))
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

        private bool PricingCalendarEntryExists(int id)
        {
            return _context.PricingCalendarEntries.Any(e => e.ID == id);
        }
    }
}
