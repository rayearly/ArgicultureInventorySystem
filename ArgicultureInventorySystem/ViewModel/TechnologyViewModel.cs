using ExportExcel.Code;
using System.Collections.Generic;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;

namespace ExportExcel.Models
{
    public class TechnologyViewModel
    {
        public List<StockViewModel> Technologies
        {
            get
            {
                return StaticData.Stocks;
            }
        }

    }
}