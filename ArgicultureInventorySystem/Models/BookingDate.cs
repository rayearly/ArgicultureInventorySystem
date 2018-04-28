using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class BookingDate
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime BookingDateTime { get; set; }

        // TODO: Detect must be after BookingDateTime
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDateTime { get; set; }
    }
}