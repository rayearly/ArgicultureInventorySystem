using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace ArgicultureInventorySystem.Models
{
    public class Booking
    {
        public Booking()
        {
            BookingStatus = "In Process";
        }

        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Key, Column(Order = 1)]
        public int StockId { get; set; }

        [ForeignKey("StockId")]
        public virtual Stock Stock { get; set; }

        // Refer to the date from BookingDate
        [Key, Column(Order = 2)]
        public int BookingDateId { get; set; }

        [ForeignKey("BookingDateId")]
        public virtual BookingDate BookingDate { get; set; }

        // Limit Booking Quantity to the CurrentQuantity from Stock
        public decimal BookingQuantity { get; set; }

        public string BookingNotes { get; set; }

        public string BookingStatus { get; set; }

        public string BookingId { get; set; }
    }
}