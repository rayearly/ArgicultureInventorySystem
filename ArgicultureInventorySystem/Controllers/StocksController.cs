using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;

namespace ArgicultureInventorySystem.Controllers
{
    [Authorize(Roles = RoleName.CanManageBookings)]
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        private void LoadMeasurementType()
        {
            var measurementTypes = _context.StockMeasurements.ToList();

            var selectItems = new List<SelectListItem>();

            foreach (var type in measurementTypes)
            {
                var listItem = new SelectListItem
                {
                    Value = type.Id.ToString(),
                    Text = type.MeasurementType
                };

                selectItems.Add(listItem);
            }

            ViewBag.MeasurementTypes = selectItems;
        }

        private void LoadTypes()
        {
            var types = _context.StockTypes.ToList();

            var selectItems = new List<SelectListItem>();

            foreach (var type in types)
            {
                var listItem = new SelectListItem
                {
                    Value = type.Id.ToString(),
                    Text = type.StockTypeAssigned
                };

                selectItems.Add(listItem);
            }

            ViewBag.Types = selectItems;
        }

        // GET: Stocks
        public ActionResult Index()
        {
            var stock = _context.Stocks.ToList();

            return View(stock);
        }

        #region GET: Different types of stocks (Display / Export Excel)
        // Pesticide = 2, Tools = 1, Fertilizer = 3

        // GET: Stocks - Pesticide
        public ActionResult GetStockPesticide()
        {
            var stockPesticide = _context.Stocks.Where(s => s.TypeId == 2).ToList();

            return View("Index", stockPesticide);
        }

        // GET: List from ViewModel to Export to Excel
        public List<StockViewModel> GetStockPesticideReport()
        {
            var stockPesticide = _context.Stocks.Where(s => s.TypeId == 2).
                Select(s => new StockViewModel
                {
                    Name = s.Name,
                    CurrentQuantity = s.CurrentQuantity,
                    OriginalQuantity = s.OriginalQuantity,
                    MeasurementName = s.Measurement.MeasurementType
                }).ToList();

            return stockPesticide;
        }
        
        // GET: Stocks - Tools
        public ActionResult GetStockFertilizer()
        {
            var stockFertilizer = _context.Stocks.Where(s => s.TypeId == 3).ToList();

            return View("Index", stockFertilizer);
        }

        // GET: List from ViewModel to Export to Excel
        public List<StockViewModel> GetStockFertilizerReport()
        {
            var stockPesticide = _context.Stocks.Where(s => s.TypeId == 3).
                Select(s => new StockViewModel
                {
                    Name = s.Name,
                    CurrentQuantity = s.CurrentQuantity,
                    OriginalQuantity = s.OriginalQuantity,
                    MeasurementName = s.Measurement.MeasurementType
                }).ToList();

            return stockPesticide;
        }

        // GET: Stocks = Fertilizers
        public ActionResult GetStockTool()
        {
            var stockTool = _context.Stocks.Where(s => s.TypeId == 1).ToList();

            return View("Index", stockTool);
        }

        // GET: List from ViewModel to Export to Excel
        public List<StockViewModel> GetStockToolReport()
        {
            var stockPesticide = _context.Stocks.Where(s => s.TypeId == 1).
                Select(s => new StockViewModel
                {
                    Name = s.Name,
                    CurrentQuantity = s.CurrentQuantity,
                    OriginalQuantity = s.OriginalQuantity,
                    MeasurementName = s.Measurement.MeasurementType
                }).ToList();

            return stockPesticide;
        }

        #endregion


        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            LoadTypes();
            LoadMeasurementType();
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,OriginalQuantity,CurrentQuantity,MeasurementId,TypeId,Note")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Stocks.Add(stock);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            LoadTypes();
            LoadMeasurementType();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,OriginalQuantity,CurrentQuantity,MeasurementId,TypeId,Note")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(stock).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = _context.Stocks.Find(id);
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Delete by StockId
        [HttpDelete]
        public ActionResult DeleteByStockId(int stockId)
        {
            // Get the bookings that have the same bookingDateId to be deleted
            var stockToDelete = _context.Stocks.Where(b => b.Id == stockId);

            if (stockToDelete.Any())
            {
                foreach (var stock in stockToDelete)
                {
                    // Delete each of the bookings
                    _context.Stocks.Remove(stock);
                }
            }
            else
            {
                // If booking does not exist return BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _context.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult GetStock(string term = "")
        {
            var measurements = _context.Stocks
                .Where(m => m.Name.ToUpper()
                .Contains(term.ToUpper()))
                .Select(m => new {Name = m.Name, ID = m.Id})
                .Distinct().ToList();

            return Json(measurements, JsonRequestBehavior.AllowGet);
        }
    }
}
