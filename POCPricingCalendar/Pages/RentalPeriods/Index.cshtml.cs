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
    public class IndexModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public IndexModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

        public IList<RentalPeriod> RentalPeriod { get;set; }

        public async Task OnGetAsync()
        {
            RentalPeriod = await _context.RentalPeriods
                .Include(r => r.Rental).ToListAsync();
        }
    }
}
