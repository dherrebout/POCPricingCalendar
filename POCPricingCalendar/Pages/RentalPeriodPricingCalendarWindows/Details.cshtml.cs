﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly PricingCalendar.Data.PricingCalendarContext _context;

        public DetailsModel(PricingCalendar.Data.PricingCalendarContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
