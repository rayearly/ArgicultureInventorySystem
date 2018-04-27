using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ArgicultureInventorySystem.Models;

namespace ArgicultureInventorySystem.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TypeId { get; set; }

        // Stock Type List : Tool / Pesticide / Fertilizer (virtual to fill the data ready)
        [ForeignKey("TypeId")]
        public virtual StockType Type { get; set; }

        // Kuantiti Dibeli
        public decimal OriginalQuantity { get; set; }

        // Kuantiti Semasa (Setelah dipinjam2?)
        public decimal CurrentQuantity { get; set; }

        public int? MeasurementId { get; set; }

        // TODO: Display number based on measurement type. If its tools, then no decimal
        [ForeignKey("MeasurementId")]
        public virtual StockMeasurement Measurement { get; set; }

        // TODO: Maybe can select specific type of problem to note for
        public string Note { get; set; }

        public IList<Booking> Bookings { get; set; }

        // TODO: Every Domain Model that is referred right here must be also converted to DTO
    }
}