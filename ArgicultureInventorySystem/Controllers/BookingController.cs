using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;
using Microsoft.Ajax.Utilities;
using FluentValidation;

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

        // GET: Booking for Specific Customer
        public ActionResult CustomerBooking(string id)
        {
            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.ApplicationUser.Id == id).ToList();

            // Get the dates from the specific booking
            var getBookingDates = getSpecificBooking.Select(b => b.BookingDate).ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                //UniversityCommunity = _context.UniversityCommunities.SingleOrDefault(u => u.Id == id),
                Bookings = getSpecificBooking,
                BookingDates = getBookingDates.Distinct(),
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == id)
            };

            return View("CustBookingList" , viewModel);
        }

        // GET: Booking List for Admin (Show list of Customer)
        [Authorize(Roles = RoleName.CanManageBookings)]
        public ActionResult AllBookingList()
        {
            var au = _context.Users.ToList();

            return View("Index", au);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create(string id)
        {
            LoadStocks();
            var uc = _context.Users.SingleOrDefault(u => u.Id == id);
            
            var viewModel = new UcBookingStockViewModel
            {
                //UniversityCommunity = uc,
                Bookings = _context.Bookings.ToList(),
                Stocks = _context.Stocks.ToList(),
                ApplicationUser = uc
            };

            return View(viewModel);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(UcBookingStockViewModel ucBooking)
        {
            /* TODO: When Creating an new Booking, check the number of stock number available, 
             * TODO: If its less than booked number, deny booking. If its allowed, substract (UPDATE) from currentValue attribute */

            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.ApplicationUser.Id == ucBooking.ApplicationUser.Id).ToList();

            var getSpecificBooking2 = ucBooking.Bookings.ToList();

            var viewModel = new UcBookingStockViewModel
            {
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == ucBooking.ApplicationUser.Id),
                Bookings = getSpecificBooking2
            };

            if (!ModelState.IsValid)
            {
                LoadStocks();
                return View("Create", viewModel);
            }
            
            foreach (var booking in ucBooking.Bookings)
            {
                booking.UserId = ucBooking.ApplicationUser.Id;
                booking.BookingDate = ucBooking.BookingDate;
                booking.Stock = booking.Stock;

                // Get stock to check current Quantity
                var getStock = _context.Stocks.Single(s => s.Id == booking.StockId);

                if (booking.BookingQuantity > getStock.CurrentQuantity)
                {
                    // Do something
                    ViewBag.OverloadBooking = "shit";
                    LoadStocks();
                    return View(ucBooking);
                }

                _context.Bookings.Add(booking);
                
            }

            _context.SaveChanges();

            var getId = viewModel.ApplicationUser.Id;

            return RedirectToAction("Index", new { id = getId });
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(string id, int bookingDateId)
        {
            LoadStocks();

            // Get the universityCommunity information by passed Id
            var uc = _context.Users.SingleOrDefault(u => u.Id == id);

            // Get the whole list of bookings
            var getBookings = uc.Bookings.ToList();

            var book = new List<Booking>(getBookings.Where(b => b.BookingDateId == bookingDateId));
            
            var viewModel = new UcBookingStockViewModel
            {
                //UniversityCommunity = uc,
                Bookings = book,
                Stocks = _context.Stocks.ToList(),
                BookingDate = _context.BookingDates.Single(b => b.Id == bookingDateId),
                ApplicationUser = uc
            };

            return View(viewModel);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UcBookingStockViewModel ucBooking)
        {
            /* TODO: When Adding/Creating a new Booking, check the number of stock number available, 
             * TODO: If its less than booked number, deny booking. If its allowed, substract (UPDATE) from currentValue attribute
             * TODO: Check for changes? */

            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.ApplicationUser.Id == ucBooking.ApplicationUser.Id).ToList();

            var viewModel = new UcBookingStockViewModel
            {
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == ucBooking.ApplicationUser.Id),
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
                    booking.UserId = ucBooking.ApplicationUser.Id;
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

            var getId = viewModel.ApplicationUser.Id;

            return RedirectToAction("Index", new { id = getId });
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int bookingDateId, int? stockId, string ucId)
        {
            if (stockId == null || ucId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the specific booking from Booking table (PK: stockId + bookingdateId + universityCommunityId)
            var booking = _context.Bookings.Single(b => b.BookingDateId == bookingDateId && b.StockId == stockId && b.UserId == ucId);
            
            return View("Edit");
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, int? stockId, string ucId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the specific booking from Booking table (PK: stockId + bookingdateId + universityCommunityId)
            var booking = _context.Bookings.Single(b => b.BookingDateId == id && b.StockId == stockId && b.UserId == ucId);

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            #region Start Loading ViewModel to return to Edit
            
            LoadStocks();

            // Get the universityCommunity information by passed Id
            var uc = _context.Users.SingleOrDefault(u => u.Id == ucId);

            // Get the whole list of bookings
            var getBookings = uc.Bookings.ToList();

            var book = new List<Booking>(getBookings.Where(b => b.BookingDateId == id));

            var viewModel = new UcBookingStockViewModel
            {
                ApplicationUser = uc,
                Bookings = book,
                Stocks = _context.Stocks.ToList(),
                BookingDate = _context.BookingDates.Single(b => b.Id == id)
            };
            #endregion

            return View("Edit", viewModel);
        }

        #region LoadStock - Get the stock into the ViewBag to be used in the PartialViews
        private void LoadStocks()
        {
            var stocks = _context.Stocks.ToList();

            // Get the stock into the list
            var selectItems = stocks.Select(stock => new SelectListItem
            {
                Value = stock.Id.ToString(),
                Text = stock.Name
            })
            .ToList();

            // Store the list in a ViewBag
            ViewBag.LoadStocks = selectItems;
        }
        #endregion

        // Function to return the partial view created for Booking
        public ActionResult BookingPartialResult()
        {
            LoadStocks();
            return PartialView("_BookingPartial", new Booking());
        }
    }
}
