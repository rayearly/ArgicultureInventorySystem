using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;
using Microsoft.Ajax.Utilities;

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

            var getBookingDates = new List<BookingDate>();

            foreach (var b in getSpecificBooking)
            {
                
                getBookingDates.Add(b.BookingDate); 
            }

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id),
                Bookings = getSpecificBooking,
                BookingDates = getBookingDates.Distinct()
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
                Stocks = _context.Stocks.ToList()
            };

            return View(viewModel);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(UcBookingStockViewModel ucBooking)
        {
            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.UniversityCommunity.Id == ucBooking.UniversityCommunity.Id).ToList();
            
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
        public ActionResult Edit(int id, int bookingDateId)
        {
            LoadStocks();

            // Get the universityCommunity information by passed Id
            var uc = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id);

            // Get the whole list of bookings
            var getBookings = uc.Bookings.ToList();

            var book = new List<Booking>(getBookings.Where(b => b.BookingDateId == bookingDateId));
            
            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = uc,
                Bookings = book,
                Stocks = _context.Stocks.ToList(),
                BookingDate = _context.BookingDates.Single(b => b.Id == bookingDateId)
        };

            return View(viewModel);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UcBookingStockViewModel ucBooking)
        {
            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.UniversityCommunity.Id == ucBooking.UniversityCommunity.Id).ToList();

            var viewModel = new UcBookingStockViewModel
            {
                UniversityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == ucBooking.UniversityCommunity.Id),
                Bookings = getSpecificBooking
            };

            if (!ModelState.IsValid)
            {
                LoadStocks();
                return View("Edit", viewModel);
            }


            foreach (var booking in ucBooking.Bookings)
            {
                if (booking.BookingDateId == 0)
                {
                    //Insert if bookingId 0
                    booking.UniversityCommunityId = ucBooking.UniversityCommunity.Id;
                    booking.Stock = booking.Stock;
                    booking.BookingDateId = ucBooking.Bookings.First().BookingDateId;
                    _context.Bookings.Add(booking);
                    
                }

                else
                {
                    //Update if exist
                    var bInDb = _context.Bookings.Single(b => b.BookingDateId == booking.BookingDateId && b.StockId == booking.StockId);

                    bInDb.BookingDate = booking.BookingDate;
                    bInDb.BookingDateId = booking.BookingDateId;
                    bInDb.BookingNotes = booking.BookingNotes;
                    bInDb.BookingQuantity = booking.BookingQuantity;
                    bInDb.Stock = booking.Stock;
                    bInDb.StockId = booking.StockId;
                }
            }

            _context.SaveChanges();

            var getId = viewModel.UniversityCommunity.Id;

            return RedirectToAction("Index", new { id = getId });
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
