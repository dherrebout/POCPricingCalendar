using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PricingCalendar.Models
{
    public class RentalPeriodPricingCalendarWindow
    {
        public int ID { get; set; }
        public int PricingCalendarEntryID { get; set; }
        public int RentalPeriodID { get; set; }
        public RentalPeriod RentalPeriod { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public decimal DurationHours { get; set; }
        [Column(TypeName = "money")]
        public decimal PricingCalendarEntryPrice { get; set; }
        //public PricingCalendarEntry PricingCalendarEntry { get; set; }
    }
}
