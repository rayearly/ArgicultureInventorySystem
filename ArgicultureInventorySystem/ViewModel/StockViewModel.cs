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

        [DisplayName("Quantity In Card")]
        public decimal QuantityInCard { get; set; }

        [DisplayName("Measurement ID")]
        public string MeasurementName { get; set; }

        [DisplayName("Note")]
        public string Note { get; set; }
    }
}