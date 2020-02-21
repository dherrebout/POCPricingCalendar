using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.PricingCalendarEntryWindows
{
    public class IndexModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public IndexModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        public IList<RentalPeriodPricingCalendarWindow> PricingCalendarEntryWindow { get;set; }

        public async Task OnGetAsync()
        {
            PricingCalendarEntryWindow = await _context.RentalPeriodPricingCalendarWindows.ToListAsync();
        }
    }
}
