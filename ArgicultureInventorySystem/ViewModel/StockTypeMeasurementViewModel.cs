using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgicultureInventorySystem.Models;

namespace ArgicultureInventorySystem.ViewModel
{
    public class StockTypeMeasurementViewModel
    {
        public IList<Stock> Stocks { get; set; }

        public StockMeasurement StockMeasurement { get; set; }

        public StockType StockType { get; set; }
    }
}