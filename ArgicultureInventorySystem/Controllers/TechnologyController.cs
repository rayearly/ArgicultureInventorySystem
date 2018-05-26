using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;
using ExportExcel.Code;
using ExportExcel.Models;

namespace ArgicultureInventorySystem.Controllers
{
    public class TechnologyController : Controller
    {
        // GET: Technology
        public ActionResult Index()
        {
            TechnologyViewModel model = new TechnologyViewModel();
            return View(model);
        }

        [HttpGet]
        public FileContentResult ExportToExcelStockPesticide()
        {
            List<StockViewModel> stocks = StaticData.StocksPesticide;
            string[] columns = { "Name", "Project", "Developer" };
            byte[] filecontent = ExcelExportHelper.ExportExcel(stocks, "Stock Report (Pesticide)", true, columns);
            return File(filecontent, ExcelExportHelper.ExcelContentType, DateTime.Now.ToString("yyyyMMdd") + " - StockReport(Pesticide).xlsx");
        }

        [HttpGet]
        public FileContentResult ExportToExcelStockTool()
        {
            List<StockViewModel> stocks = StaticData.StocksTool;
            string[] columns = { "Name", "Project", "Developer" };
            byte[] filecontent = ExcelExportHelper.ExportExcel(stocks, "Stock Report (Tools)", true, columns);
            return File(filecontent, ExcelExportHelper.ExcelContentType, DateTime.Now.ToString("yyyyMMdd") + " - StockReport(Tools).xlsx");
        }

        [HttpGet]
        public FileContentResult ExportToExcelStockFertilizer()
        {
            List<StockViewModel> stocks = StaticData.StocksFertilizer;
            string[] columns = { "Name", "Project", "Developer" };
            byte[] filecontent = ExcelExportHelper.ExportExcel(stocks, "Stock Report (Fertilizers)", true, columns);
            return File(filecontent, ExcelExportHelper.ExcelContentType, DateTime.Now.ToString("yyyyMMdd") + " - StockReport(Fertilizers).xlsx");
        }
    }
}