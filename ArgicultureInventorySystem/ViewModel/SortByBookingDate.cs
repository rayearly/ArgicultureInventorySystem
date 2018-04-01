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

/* Where BookingDateID == bookingdateId for University bookings list 
  ---- get the booking details inside the newly created booking viewmodel?
  ---- bookingdate id
  -------- list of booking that had the bookingdate id
     
     */