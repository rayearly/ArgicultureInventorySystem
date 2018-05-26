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
    public class DepartmentFacultiesController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: DepartmentFaculties
        public ActionResult Index()
        {
            return View(_context.DepartmentFaculties.ToList());
        }

        // GET: DepartmentFaculties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentFaculty departmentFaculty = _context.DepartmentFaculties.Find(id);
            if (departmentFaculty == null)
            {
                return HttpNotFound();
            }
            return View(departmentFaculty);
        }

        // GET: DepartmentFaculties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentFaculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] DepartmentFaculty departmentFaculty)
        {
            if (ModelState.IsValid)
            {
                _context.DepartmentFaculties.Add(departmentFaculty);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departmentFaculty);
        }

        // GET: DepartmentFaculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentFaculty departmentFaculty = _context.DepartmentFaculties.Find(id);
            if (departmentFaculty == null)
            {
                return HttpNotFound();
            }
            return View(departmentFaculty);
        }

        // POST: DepartmentFaculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] DepartmentFaculty departmentFaculty)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(departmentFaculty).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departmentFaculty);
        }

        // GET: DepartmentFaculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentFaculty departmentFaculty = _context.DepartmentFaculties.Find(id);
            if (departmentFaculty == null)
            {
                return HttpNotFound();
            }
            return View(departmentFaculty);
        }

        // POST: DepartmentFaculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentFaculty departmentFaculty = _context.DepartmentFaculties.Find(id);
            _context.DepartmentFaculties.Remove(departmentFaculty ?? throw new InvalidOperationException());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Delete by StockId
        [HttpDelete]
        public ActionResult DeleteByDfId(int dfId)
        {
            // Get the bookings that have the same bookingDateId to be deleted
            var dfToDelete = _context.DepartmentFaculties.Where(b => b.Id == dfId);

            if (dfToDelete.Any())
            {
                foreach (var df in dfToDelete)
                {
                    // Delete each of the bookings
                    _context.DepartmentFaculties.Remove(df);
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
    }
}
