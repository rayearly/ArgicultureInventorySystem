using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class StockMeasurement
    {
        public int Id { get; set; }

        // KG / (TONG / LITRE) / BOX / BAG / BOTTLE / PACK / LITRE / TONG
        [Required]
        [DisplayName("Measurement Type")]
        public string MeasurementType { get; set; }
    }
}