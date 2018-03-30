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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
