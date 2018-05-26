using System.Collections.Generic;
using System.Linq;
using System.Net;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.Controllers;
using ArgicultureInventorySystem.ViewModel;

namespace ExportExcel.Code
{
    public class StaticData
    {
        public static List<Technology> Technologies
        {
            get
            {
                return new List<Technology>{
                    new Technology{Name="ASP.NET", Project=12,Developer=50, TeamLeader=6},
                    new Technology{Name="Php", Project=40,Developer=60, TeamLeader=9},
                    new Technology{Name="iOS", Project=11,Developer=5, TeamLeader=1},
                    new Technology{Name="Android", Project=20,Developer=26, TeamLeader=2}
                };
            }
        }

        public static List<StockViewModel> Stocks
        {
            get
            {
                var stc = new StocksController();

                return stc.GetStockPesticideReport();
                
            }
        }
    }
}