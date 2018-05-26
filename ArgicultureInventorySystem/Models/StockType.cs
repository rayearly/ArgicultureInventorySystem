using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArgicultureInventorySystem.Models
{
    public class StockType
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Type Name")]
        public string StockTypeAssigned { get; set; }

        // Stock Type : Tool / Pesticide / Fertilizer (Many more specifics...)
    }
}