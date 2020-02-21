using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.PricingCalendarEntries
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
            return Page();
        }

        [BindProperty]
        public PricingCalendarEntry PricingCalendarEntry { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PricingCalendarEntries.Add(PricingCalendarEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
