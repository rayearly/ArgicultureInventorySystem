using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class StockType
    {
        public int Id { get; set; }

        public string StockTypeAssigned { get; set; }

        // Stock Type : Tool / Pesticide / Fertilizer (Many more specifics...)
    }
}