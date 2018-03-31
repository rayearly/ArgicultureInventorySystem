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
            var getBooking = _context.Bookings.ToList();

            var getSpecificBooking = new List<Booking>();

            // Get Bookings specific to the customer
            foreach (var b in getBooking)
            {
                if (b.UniversityCommunity.Id == id)
                {
                    getSpecificBooking.Add(b);
                }
            }

            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id),
                Bookings = getSpecificBooking
            };

            return View("IndexSortByBooking" , viewModel);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create(int id)
        {
            LoadStocks();
            var uc = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id);
            //var booking = _context.Bookings.Single(u => u.UniversityCommunity.Id == id).BookingDate;

            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = uc,
                Bookings = _context.Bookings.ToList(),
                Stocks = _context.Stocks.ToList(),
                //BookingDate = booking
            };

            return View(viewModel);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(UcBookingStockViewModel ucBooking)
        {
            var getBooking = _context.Bookings.ToList();

            var getSpecificBooking = new List<Booking>();

            // Get Bookings specific to the customer
            foreach (var b in getBooking)
            {
                if (b.UniversityCommunity.Id == ucBooking.UniversityCommunity.Id)
                {
                    getSpecificBooking.Add(b);
                }
            }

            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == ucBooking.UniversityCommunity.Id),
                Bookings = getSpecificBooking
            };

            if (!ModelState.IsValid)
            {
                LoadStocks();
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

            var getId = viewModel.UniversityCommunity.Id;

            return RedirectToAction("Index", new { id = getId });
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
            LoadStocks();
            return PartialView("_BookingPartial", new Booking());
        }
    }
}
