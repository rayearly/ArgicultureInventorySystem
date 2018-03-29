using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgicultureInventorySystem.Models;

namespace ArgicultureInventorySystem.ViewModel
{
    public class StockMeasurementViewModel
    {
        public Stock Stock { get; set; }

        public StockMeasurement StockMeasurement { get; set; }
    }
}