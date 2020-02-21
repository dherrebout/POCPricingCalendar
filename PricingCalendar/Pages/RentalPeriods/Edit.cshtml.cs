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

namespace PricingCalendar.Pages.RentalPeriods
{
    public class EditModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public EditModel(PricingCalendar.Data.PricingCalendarContext context)
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
           ViewData["RentalID"] = new SelectList(_context.Rentals, "ID", "ID");
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

            _context.Attach(RentalPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalPeriodExists(RentalPeriod.ID))
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

        private bool RentalPeriodExists(int id)
        {
            return _context.RentalPeriods.Any(e => e.ID == id);
        }
    }
}
