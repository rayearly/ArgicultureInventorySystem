using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;

namespace ArgicultureInventorySystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Booking
        public ActionResult Index(int id)
        {
            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id),
                Bookings = _context.Bookings.ToList()
            };


            return View(viewModel);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create(int id)
        {
            var uc = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id);
            //var booking = _context.Bookings.SingleOrDefault(u => u.UniversityCommunity.Id == ucId);
            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = uc,
                Bookings = _context.Bookings.ToList(),
                Stocks = _context.Stocks.ToList()
            };

            return View(viewModel);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(UcBookingStockViewModel ucBooking)
        {
            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == ucBooking.UniversityCommunity.Id),

            };

            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            foreach (var booking in ucBooking.Bookings)
            {
                booking.UniversityCommunityId = ucBooking.UniversityCommunity.Id;
                booking.BookingDate = ucBooking.BookingDate;
                booking.Stock = booking.Stock;
                _context.Bookings.Add(booking);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Booking");
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
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

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void LoadStocks()
        {
            var stocks = _context.Stocks.ToList();

            var selectItems = new List<SelectListItem>();

            foreach (var stock in stocks)
            {
                var listItem = new SelectListItem
                {
                    Value = stock.Id.ToString(),
                    Text = stock.Name
                };

                selectItems.Add(listItem);
            }

            ViewBag.LoadStocks = selectItems;
        }

        public ActionResult BookingPartialResult()
        {
            //var viewModel = new JudgeBoothMarkViewModel
            //{
            //JudgeBoothMarks = _context.JudgeBoothMark.ToList(),
            //Booths = _context.Booths.ToList()
            //};
            LoadStocks();
            return PartialView("_BookingPartial", new Booking());
        }
    }
}
