using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgicultureInventorySystem.Models;

namespace ArgicultureInventorySystem.ViewModel
{
    public class SortByBookingDate
    {
        public BookingDate BookingDate { get; set; }

        public virtual IList<Booking> Bookings { get; set; }
    }
}