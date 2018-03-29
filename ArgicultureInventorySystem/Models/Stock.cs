using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TypeId { get; set; }

        // Stock Type List : Tool / Pesticide / Fertilizer
        [ForeignKey("TypeId")]
        public StockType Type { get; set; }

        // Kuantiti Dibeli
        public decimal OriginalQuantity { get; set; }

        // Kuantiti Semasa (Setelah dipinjam2?)
        public decimal CurrentQuantity { get; set; }

        public int? MeasurementId { get; set; }

        // TODO: Display number based on measurement type. If its tools, then no decimal
        [ForeignKey("MeasurementId")]
        public StockMeasurement Measurement { get; set; }

        // TODO: Maybe can select specific type of problem to note for
        public string Note { get; set; }

        public IList<Booking> Bookings { get; set; }
    }
}