using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace ArgicultureInventorySystem.Models
{
    public class Booking
    {
        [Key, Column(Order = 0)]
        public int UniversityCommunityId { get; set; }

        [ForeignKey("UniversityCommunityId")]
        public virtual UniversityCommunity UniversityCommunity { get; set; }

        [Key, Column(Order = 1)]
        public int StockId { get; set; }

        [ForeignKey("StockId")]
        public virtual Stock Stock { get; set; }

        // Refer to the date from BookingDate
        public int BookingDateId { get; set; }

        [ForeignKey("BookingDateId")]
        public virtual BookingDate BookingDate { get; set; }

        // Limit Booking Quantity to the CurrentQuantity from Stock
        public decimal BookingQuantity { get; set; }

        public string BookingNotes { get; set; }

    }
}