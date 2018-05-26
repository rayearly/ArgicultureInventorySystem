using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ArgicultureInventorySystem.Models;

namespace ArgicultureInventorySystem.ViewModel
{
    public class UcBookingStockViewModel
    {
        //public UniversityCommunity UniversityCommunity { get; set; }

        public IList<Stock> Stocks { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual BookingDate BookingDate { get; set; }

        // Get Booking by BookingDate
        public virtual IEnumerable<BookingDate> BookingDates { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        public string BookingId { get; set; }

        public string BookingStatus { get; set; }
    }
}