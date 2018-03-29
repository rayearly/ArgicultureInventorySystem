using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class Booking
    {
        [Key, Column(Order = 0)]
        public int UniversityCommunityId { get; set; }

        public virtual UniversityCommunity UniversityCommunity { get; set; }

        [Key, Column(Order = 1)]
        public int StockId { get; set; }

        public virtual Stock Stock { get; set; }

        // Limit Booking Quantity to the CurrentQuantity from Stock
        public decimal BookingQuantity { get; set; }

        public DateTime BookingDateTime { get; set; }

        public DateTime ReturnDateTime { get; set; }
    }
}