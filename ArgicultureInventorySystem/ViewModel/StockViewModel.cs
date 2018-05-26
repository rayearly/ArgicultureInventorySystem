using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.ViewModel
{
    public class StockViewModel
    {
        [DisplayName("Stock Name")]
        public string Name { get; set; }

        [DisplayName("Current Quantity")]
        public decimal CurrentQuantity { get; set; }

        [DisplayName("Original Quantity")]
        public decimal OriginalQuantity { get; set; }

        [DisplayName("Measurement ID")]
        public string MeasurementName { get; set; }
    }
}