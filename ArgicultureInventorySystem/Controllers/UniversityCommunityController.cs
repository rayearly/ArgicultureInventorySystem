using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;

namespace ArgicultureInventorySystem.Controllers
{
    public class UniversityCommunityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UniversityCommunityController()
        {
            _context = new ApplicationDbContext();    
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: UniversityCommunity
        public ActionResult Index()
        {
            var universityCommunity = _context.Users.ToList();

            return View(universityCommunity);
        }

        // GET: UniversityCommunity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UniversityCommunity/Create
        public ActionResult Create()
        {
            var universityCommunity = new UniversityCommunity();
            return View("Create", universityCommunity);
        }

        // POST: UniversityCommunity/Create
        [HttpPost]
        public ActionResult Create(UniversityCommunity universityCommunity)
        {
            try
            {
                // TODO: Add insert logic here

                // Check whether data entered valid or not
                if (!ModelState.IsValid)
                {
                    return View("Create", universityCommunity);
                }

                // Create New
                if (universityCommunity.Id == 0)
                {
                    _context.UniversityCommunities.Add(universityCommunity);
                }

                // Update
                else
                {
                    var ucInDb = _context.UniversityCommunities.Single(u => u.Id == universityCommunity.Id);

                    ucInDb.Name = universityCommunity.Name;
                    ucInDb.IdNumber = universityCommunity.IdNumber;
                    ucInDb.PhoneNumber = universityCommunity.PhoneNumber;
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "UniversityCommunity");
            }

            catch
            {
                return HttpNotFound();
            }
        }

        // GET: UniversityCommunity/Edit/5
        public ActionResult Edit(int id)
        {
            var universityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id);

            if (universityCommunity == null)
                return HttpNotFound();

            return View("Create", universityCommunity);
        }

        // POST: UniversityCommunity/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UniversityCommunity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UniversityCommunity uc = _context.UniversityCommunities.Find(id);

            if (uc == null)
                return HttpNotFound();

            return View(uc);
        }

        // POST: UniversityCommunity/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var universityCommunityInDb = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id);

                if (universityCommunityInDb == null)
                    return HttpNotFound();

                _context.UniversityCommunities.Remove(universityCommunityInDb);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
