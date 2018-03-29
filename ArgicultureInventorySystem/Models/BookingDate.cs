using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class BookingDate
    {
        public int Id { get; set; }

        public DateTime BookingDateTime { get; set; }

        public DateTime ReturnDateTime { get; set; }
    }
}