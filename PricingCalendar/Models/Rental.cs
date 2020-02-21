using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PricingCalendar.Models
{
    public class Rental
    {
        public int ID { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        [Column(TypeName = "money")]
        public decimal? BasePrice { get; set; }
        public List<RentalPeriod> RentalPeriods { get; set; }
    }
}
