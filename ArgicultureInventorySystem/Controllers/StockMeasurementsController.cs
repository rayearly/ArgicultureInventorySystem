using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArgicultureInventorySystem.Models;

namespace ArgicultureInventorySystem.Controllers
{
    public class StockMeasurementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StockMeasurements
        public ActionResult Index()
        {
            return View(db.StockMeasurements.ToList());
        }

        // GET: StockMeasurements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockMeasurement stockMeasurement = db.StockMeasurements.Find(id);
            if (stockMeasurement == null)
            {
                return HttpNotFound();
            }
            return View(stockMeasurement);
        }

        // GET: StockMeasurements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockMeasurements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MeasurementType")] StockMeasurement stockMeasurement)
        {
            if (ModelState.IsValid)
            {
                db.StockMeasurements.Add(stockMeasurement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockMeasurement);
        }

        // GET: StockMeasurements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockMeasurement stockMeasurement = db.StockMeasurements.Find(id);
            if (stockMeasurement == null)
            {
                return HttpNotFound();
            }
            return View(stockMeasurement);
        }

        // POST: StockMeasurements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MeasurementType")] StockMeasurement stockMeasurement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockMeasurement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockMeasurement);
        }

        // GET: StockMeasurements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockMeasurement stockMeasurement = db.StockMeasurements.Find(id);
            if (stockMeasurement == null)
            {
                return HttpNotFound();
            }
            return View(stockMeasurement);
        }

        // POST: StockMeasurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockMeasurement stockMeasurement = db.StockMeasurements.Find(id);
            db.StockMeasurements.Remove(stockMeasurement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
