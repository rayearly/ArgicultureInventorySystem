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
        public FileContentResult ExportToExcel()
        {
            List<StockViewModel> technologies = StaticData.Stocks;
            string[] columns = { "Name", "Project", "Developer" };
            byte[] filecontent = ExcelExportHelper.ExportExcel(technologies, "Technology", true, columns);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "Technologies.xlsx");
        }
    }
}