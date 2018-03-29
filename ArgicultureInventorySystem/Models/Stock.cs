using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Stock Type List : Tool / Pesticide / Fertilizer
        public StockType Type { get; set; }

        // Kuantiti Dibeli
        public decimal OriginalQuantity { get; set; }

        // Kuantiti Semasa (Setelah dipinjam2?)
        public decimal  CurrentQuantity { get; set; }

        public StockMeasurement Measurement { get; set; }

        public string Note { get; set; }
    }
}