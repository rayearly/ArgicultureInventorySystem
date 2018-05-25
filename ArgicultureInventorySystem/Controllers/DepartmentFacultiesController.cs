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
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: DepartmentFaculties
        public ActionResult Index()
        {
            return View(_db.DepartmentFaculties.ToList());
        }

        // GET: DepartmentFaculties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentFaculty departmentFaculty = _db.DepartmentFaculties.Find(id);
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
                _db.DepartmentFaculties.Add(departmentFaculty);
                _db.SaveChanges();
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
            DepartmentFaculty departmentFaculty = _db.DepartmentFaculties.Find(id);
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
                _db.Entry(departmentFaculty).State = EntityState.Modified;
                _db.SaveChanges();
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
            DepartmentFaculty departmentFaculty = _db.DepartmentFaculties.Find(id);
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
            DepartmentFaculty departmentFaculty = _db.DepartmentFaculties.Find(id);
            _db.DepartmentFaculties.Remove(departmentFaculty ?? throw new InvalidOperationException());
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
