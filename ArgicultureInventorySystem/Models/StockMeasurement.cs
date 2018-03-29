using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class StockMeasurement
    {
        public int Id { get; set; }

        // KG / (TONG / LITRE) / BOX / BAG / BOTTLE / PACK / LITRE / TONG
        public string MeasurementType { get; set; }
    }
}