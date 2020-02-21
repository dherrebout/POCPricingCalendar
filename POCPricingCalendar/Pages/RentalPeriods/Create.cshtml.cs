using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.RentalPeriods
{
    public class CreateModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public CreateModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RentalID"] = new SelectList(_context.Rentals, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public RentalPeriod RentalPeriod { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RentalPeriods.Add(RentalPeriod);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
