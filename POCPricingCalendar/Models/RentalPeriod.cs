using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PricingCalendar.Models
{
    public class RentalPeriod
    {
        public int ID { get; set; }
        public int RentalID { get; set; }
        public Rental Rental { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public decimal DurationHours { get; set; }
        public int PartialRentalPeriodThresholdInHours { get; set; }
        public decimal PartialRentalPeriodPriceFraction { get; set; }
        [Column(TypeName = "money")]
        public decimal WeightedPrice { get; set; }
        public List<RentalPeriodPricingCalendarWindow> RentalPeriodPricingCalendarWindows { get; set; }
    }
}
