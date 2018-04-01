using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class UniversityCommunity
    {
        public int Id { get; set; }

        public string IdNumber { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public virtual IList<Booking> Bookings { get; set; }
    }
}