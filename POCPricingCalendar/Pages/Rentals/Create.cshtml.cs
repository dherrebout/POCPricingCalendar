using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PricingCalendar.Data;
using PricingCalendar.Models;

namespace PricingCalendar.Pages.Rentals
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
        public Rental Rental { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var rentalTimeSpan = Rental.EndAt - Rental.StartAt;
            var nrRentalPeriods = rentalTimeSpan.Days + 1;
            var rentalPeriods = new List<RentalPeriod> {};

            for (int i = 1; i < nrRentalPeriods; i++) /// loop through full 24hr buckets
            {
                var rentalPeriodStart = Rental.StartAt.AddDays(i - 1);
                var rentalPeriodEnd = Rental.StartAt.AddDays(i).AddSeconds(-1);
                var rentalPeriodDuration = (decimal)(rentalPeriodEnd - rentalPeriodStart).TotalHours;

                var pricingCalendarEntries = _context.PricingCalendarEntries.Where(b => b.Date >= rentalPeriodStart.Date && b.Date <= rentalPeriodEnd.Date).ToList();
                var rentalPeriodPricingCalendarWindows = new List<RentalPeriodPricingCalendarWindow> { };

                var weightedPrice = 0.00m;

                if (pricingCalendarEntries.Count == 1)
                {
                    var w = new RentalPeriodPricingCalendarWindow
                    {
                        StartAt = rentalPeriodStart,
                        EndAt = rentalPeriodEnd,
                        DurationHours = (decimal)(rentalPeriodEnd - rentalPeriodStart).TotalHours,
                        PricingCalendarEntryPrice = pricingCalendarEntries.Find(p => p.Date == rentalPeriodStart.Date).Price
                    };
                    rentalPeriodPricingCalendarWindows.Add(w);
                }
                else
                {
                    var w1 = new RentalPeriodPricingCalendarWindow
                    {
                        StartAt = rentalPeriodStart,
                        EndAt = rentalPeriodStart.Date.AddDays(1).AddSeconds(-1),
                        DurationHours = (decimal)(rentalPeriodStart.Date.AddDays(1).AddSeconds(-1) - rentalPeriodStart).TotalHours,
                        PricingCalendarEntryPrice = pricingCalendarEntries.Find(p => p.Date == rentalPeriodStart.Date).Price
                    };
                    rentalPeriodPricingCalendarWindows.Add(w1);

                    var w2 = new RentalPeriodPricingCalendarWindow
                    {
                        StartAt = rentalPeriodStart.Date.AddDays(1),
                        EndAt = rentalPeriodEnd,
                        DurationHours = (decimal)(rentalPeriodEnd - rentalPeriodStart.Date.AddDays(1)).TotalHours,
                        PricingCalendarEntryPrice = pricingCalendarEntries.Find(p => p.Date == rentalPeriodEnd.Date).Price
                    };
                    rentalPeriodPricingCalendarWindows.Add(w2);
                }

                foreach (RentalPeriodPricingCalendarWindow window in rentalPeriodPricingCalendarWindows) // Loop over windows within rental period to calculate weighted price
                {
                    weightedPrice += (window.PricingCalendarEntryPrice * (window.DurationHours / rentalPeriodDuration));
                }

                if (rentalPeriodDuration < Settings.PartialRentalPeriodThresholdInHours)
                {
                    weightedPrice *= Settings.PartialRentalPeriodPriceFraction;
                }

                var p = new RentalPeriod
                {
                    StartAt = rentalPeriodStart,
                    EndAt = rentalPeriodEnd,
                    DurationHours = rentalPeriodDuration,
                    PartialRentalPeriodThresholdInHours = Settings.PartialRentalPeriodThresholdInHours,
                    PartialRentalPeriodPriceFraction = Settings.PartialRentalPeriodPriceFraction,
                    WeightedPrice = weightedPrice,
                    RentalPeriodPricingCalendarWindows = rentalPeriodPricingCalendarWindows
                };
                rentalPeriods.Add(p);
            }
            // check if there's a bucket < 24hrs left
            if (rentalTimeSpan.Hours != 0 || rentalTimeSpan.Minutes != 0) 
            {
                var rentalPeriodStart = Rental.StartAt.AddDays(nrRentalPeriods - 1);
                var rentalPeriodEnd = Rental.StartAt.AddDays(nrRentalPeriods - 1).AddHours(rentalTimeSpan.Hours).AddMinutes(rentalTimeSpan.Minutes).AddSeconds(-1);
                var rentalPeriodDuration = (decimal)(rentalPeriodEnd - rentalPeriodStart).TotalHours;

                var rentalPeriodPricingCalendarWindows = new List<RentalPeriodPricingCalendarWindow> { };
                var pricingCalendarEntries = _context.PricingCalendarEntries.Where(b => b.Date >= rentalPeriodStart.Date && b.Date <= rentalPeriodEnd.Date).ToList();

                var weightedPrice = 0.00m;

                if (pricingCalendarEntries.Count == 1)
                {
                    var w = new RentalPeriodPricingCalendarWindow
                    {
                        StartAt = rentalPeriodStart,
                        EndAt = rentalPeriodEnd,
                        DurationHours = (decimal)(rentalPeriodEnd - rentalPeriodStart).TotalHours,
                        PricingCalendarEntryPrice = pricingCalendarEntries[rentalPeriodPricingCalendarWindows.Count].Price
                    };
                    rentalPeriodPricingCalendarWindows.Add(w);
                }
                else
                {
                    var w1 = new RentalPeriodPricingCalendarWindow
                    {
                        StartAt = rentalPeriodStart,
                        EndAt = rentalPeriodStart.Date.AddDays(1).AddSeconds(-1),
                        DurationHours = (decimal)(rentalPeriodStart.Date.AddDays(1).AddSeconds(-1) - rentalPeriodStart).TotalHours,
                        PricingCalendarEntryPrice = pricingCalendarEntries.Find(p => p.Date == rentalPeriodStart.Date).Price
                    };
                    rentalPeriodPricingCalendarWindows.Add(w1);

                    var w2 = new RentalPeriodPricingCalendarWindow
                    {
                        StartAt = rentalPeriodStart.Date.AddDays(1),
                        EndAt = rentalPeriodEnd,
                        DurationHours = (decimal)(rentalPeriodEnd - rentalPeriodStart.Date.AddDays(1)).TotalHours,
                        PricingCalendarEntryPrice = pricingCalendarEntries.Find(p => p.Date == rentalPeriodEnd.Date).Price
                    };
                    rentalPeriodPricingCalendarWindows.Add(w2);
                }

                var nrOfWindows = rentalPeriodPricingCalendarWindows.Count;
                foreach (RentalPeriodPricingCalendarWindow window in rentalPeriodPricingCalendarWindows) // Loop over windows within rental period to calculate weighted price
                {
                    weightedPrice += (window.PricingCalendarEntryPrice * (window.DurationHours / rentalPeriodDuration));
                }

                if (rentalPeriodDuration < Settings.PartialRentalPeriodThresholdInHours)
                {
                    weightedPrice *= Settings.PartialRentalPeriodPriceFraction;
                }

                var p = new RentalPeriod
                {
                    StartAt = rentalPeriodStart,
                    EndAt = rentalPeriodEnd,
                    DurationHours = rentalPeriodDuration,
                    PartialRentalPeriodThresholdInHours = Settings.PartialRentalPeriodThresholdInHours,
                    PartialRentalPeriodPriceFraction = Settings.PartialRentalPeriodPriceFraction,
                    WeightedPrice = weightedPrice,
                    RentalPeriodPricingCalendarWindows = rentalPeriodPricingCalendarWindows
                };
                rentalPeriods.Add(p);
            }

            var basePrice = 0.00m;
            foreach (RentalPeriod rp in rentalPeriods)
            {
                basePrice += rp.WeightedPrice;
            }

            basePrice = Math.Round(basePrice * 20) / 20;

            Rental.BasePrice = basePrice;
            Rental.RentalPeriods = rentalPeriods;
            _context.Rentals.Add(Rental);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
