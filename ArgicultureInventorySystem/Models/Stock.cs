using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Stock Type")]
        public int? TypeId { get; set; }

        [Required]
        // Stock Type List : Tool / Pesticide / Fertilizer (virtual to fill the data ready)
        [ForeignKey("TypeId")]
        public virtual StockType Type { get; set; }

        [Required]
        // Kuantiti Dibeli
        [DisplayName("Original Quantity")]
        public decimal OriginalQuantity { get; set; }

        // Kuantiti Semasa (Setelah dipinjam2?)
        [DisplayName("Current Quantity")]
        public decimal CurrentQuantity { get; set; }

        // Kuantiti Dalam Kad Petak
        [DisplayName("Quantity In Card")]
        public decimal QuantityInCard { get; set; }

        [DisplayName("Measurement Type")]
        public int? MeasurementId { get; set; }

        // TODO: Display number based on measurement type. If its tools, then no decimal
        [ForeignKey("MeasurementId")]
        public virtual StockMeasurement Measurement { get; set; }

        // TODO: Maybe can select specific type of problem to note for
        public string Note { get; set; }

        public IList<Booking> Bookings { get; set; }
    }
}