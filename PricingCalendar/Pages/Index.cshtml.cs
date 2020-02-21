using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;
        public IndexModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        public IList<PricingCalendarEntry> PricingCalendarEntry { get; set; }
        public IList<Rental> Rental { get; set; }
        public IList<RentalPeriod> RentalPeriod { get; set; }
        public IList<RentalPeriodPricingCalendarWindow> RentalPeriodPricingCalendarWindow { get; set; }

        public async Task OnGetAsync()
        {
            PricingCalendarEntry = await _context.PricingCalendarEntries.OrderBy(p => p.Date).ToListAsync();
            Rental = await _context.Rentals.ToListAsync();
            RentalPeriod = await _context.RentalPeriods.ToListAsync();
            RentalPeriodPricingCalendarWindow = await _context.RentalPeriodPricingCalendarWindows.ToListAsync();
        }
    }
}
