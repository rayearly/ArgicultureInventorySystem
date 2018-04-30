using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using ArgicultureInventorySystem.Models;
using ArgicultureInventorySystem.ViewModel;
using Microsoft.Ajax.Utilities;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

        public ActionResult RedirectUserHome(string id)
        {
            // TODO: Create Admin Menu
            return User.IsInRole(RoleName.CanManageBookings) ? AdminHomePage() : CustomerBooking(id);
        }

        // DISPLAY: Menu for Admin
        [Authorize(Roles = RoleName.CanManageBookings)]
        public ViewResult AdminHomePage()
        {
            // Get Bookings specific to the customer
            var bookings = _context.Bookings.ToList();

            var viewModel = new UcBookingStockViewModel
            {
                Bookings = bookings.DistinctBy(b => b.BookingDateId)
            };

            return View("AdminHomePage", viewModel);
        }

        public ActionResult RedirectUserBooking(string id)
        {
            return User.IsInRole(RoleName.CanManageBookings) ? AdminBooking(id) : CustomerBooking(id);
        }

        // GET: Booking for Specific Customer
        public ActionResult CustomerBooking(string id)
        {
            if ((string) Session["UserSessionId"] == null)
            {
                // TODO: Get rid of admin@admin?
                return RedirectToAction("Login", "Account");
            }

            // TODO: How to clear session value after logout?
            if (id == null)
            {
                id = (string)Session["UserSessionId"];
            }

            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.ApplicationUser.Id == id).ToList();

            // Get the dates from the specific booking
            var getBookingDates = getSpecificBooking.Select(b => b.BookingDate).ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = getSpecificBooking.DistinctBy(b => b.BookingDateId),
                BookingDates = getBookingDates.Distinct(),
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == id)
            };

            return View("CustBookingList", viewModel);

        }

        // Admin Booking : same as Customer except redirected to another page? Fix this
        [Authorize(Roles = RoleName.CanManageBookings)]
        public ActionResult AdminBooking(string id)
        {
            if ((string)Session["UserSessionId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                id = (string)Session["UserSessionId"];
            }

            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.ApplicationUser.Id == id).ToList();

            // Get the dates from the specific booking
            var getBookingDates = getSpecificBooking.Select(b => b.BookingDate).ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = getSpecificBooking.DistinctBy(b => b.BookingDateId),
                BookingDates = getBookingDates.Distinct(),
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == id)
            };

            return View("AdminBookingList", viewModel);
        }

        // GET: Booking List for Admin (Show list of Customer)
        [Authorize(Roles = RoleName.CanManageBookings)]
        public ActionResult AllBookingList()
        {
            var uc = _context.Users.ToList();

            return View("Index", uc);
        }

        // GET: Get Unapproved Booking and display it in a page to be approved by admin
        public ActionResult UnApprovedBookings()
        {
            // Get the unapproved bookings
            var unapproved = _context.Bookings.Where(b => b.BookingStatus == "In Process").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = unapproved.DistinctBy(b => b.BookingDateId)
            };

            return View("UnapprovedBookingList", viewModel);
        }

        // GET: Get Unapproved Booking and display it in a page to be approved by admin
        public ActionResult ApprovedBooking()
        {
            // Get the unapproved bookings
            var unapproved = _context.Bookings.Where(b => b.BookingStatus == "Approved").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = unapproved.DistinctBy(b => b.BookingDateId)
            };

            return View("ApprovedBookingList", viewModel);
        }

        // GET: Get Unapproved Booking and display it in a page to be approved by admin
        public ActionResult RejectedBooking()
        {
            // Get the unapproved bookings
            var unapproved = _context.Bookings.Where(b => b.BookingStatus == "Rejected").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = unapproved.DistinctBy(b => b.BookingDateId)
            };

            return View("RejectedBookingList", viewModel);
        }


        [HttpPut]
        public ActionResult ApproveBooking(int bookingId)
        {
            var booking = _context.Bookings.Where(b => b.BookingDateId == bookingId);
            var stock = _context.Stocks.ToList();

            foreach (var book in booking)
            {
                foreach (var s in stock)
                {
                    if (s.Id == book.StockId)
                    {
                        // Substract current quantity with quantity booked
                        // TODO: check if the value is negative/zero, dont make approval
                        var currentQty = s.CurrentQuantity - book.BookingQuantity;

                        if (currentQty <= 0)
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                        s.CurrentQuantity = currentQty;
                    }
                }

                // Set approval (bool) into true - approved
                book.BookingStatus = "Approved";
            }

            _context.SaveChanges();

            // Get the unapproved bookings
            var unapproved = _context.Bookings.Where(b => b.BookingStatus == "In Process").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = unapproved.DistinctBy(b => b.BookingDateId)
            };

            return View("UnapprovedBookingList", viewModel);
        }

        [HttpPost]
        public ActionResult RejectBooking(int bookingId)
        {
            var booking = _context.Bookings.Where(b => b.BookingDateId == bookingId);

            foreach (var book in booking)
            {
                // Set approval (bool) into true - approved
                book.BookingStatus = "Rejected";
            }

            _context.SaveChanges();

            // Get the unapproved bookings
            var unapproved = _context.Bookings.Where(b => b.BookingStatus == "In Process").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = unapproved.DistinctBy(b => b.BookingDateId)
            };

            return View("UnapprovedBookingList", viewModel);
        }

        // TODO: CurrentQuantity plus the stock being booked?
        [HttpPost]
        public ActionResult BookingReturned(int bookingId)
        {
            return View();
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            
            var getBookingUser = _context.Bookings.First(b => b.BookingDateId == id);

            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (user.Id != getBookingUser.UserId)
            {
                return RedirectToAction("CustomerBooking", "Booking", new { user.Id });
            }

            var booking = _context.Bookings.Where(b => b.BookingDateId == id);

            var stock = _context.Stocks.ToList();

            var viewModel = new UcBookingStockViewModel
            {
                Bookings = booking,
                Stocks = stock
            };

            return View("Details", viewModel);
        }

        // GET: Booking/Create
        public ActionResult Create(string id)
        {
            LoadStocks();
            var uc = _context.Users.SingleOrDefault(u => u.Id == id);
            var bookings = new List<Booking>();

            var viewModel = new UcBookingStockViewModel
            {
                Bookings = bookings,
                Stocks = _context.Stocks.ToList(),
                ApplicationUser = uc
            };

            return View(viewModel);
        }

        // TODO: CREATE & EDIT DO NOT ALLOW SAME SELECTION OF STOCK
        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(UcBookingStockViewModel ucBooking)
        {
            /* TODO: When Creating an new Booking, check the number of stock number available, 
             * TODO: If its less than booked number, deny booking. If its allowed, substract (UPDATE) from currentValue attribute 
               TODO: Check if the stock selected is the same as previous one in the list or not. If same, deny booking.
             */

            var hold = ucBooking;

            if (!ModelState.IsValid)
            {
                LoadStocks();
                return View("Create", ucBooking);
            }

            string check = null;
            string listOfOverload = null;
            var holdBookings = new List<Booking>();

            // Generate Random BookingId to be used as reference
            var bookingId = GenerateRandomBookingId();

            foreach (var booking in ucBooking.Bookings)
            {
                booking.UserId = ucBooking.ApplicationUser.Id;
                booking.BookingDate = ucBooking.BookingDate;
                booking.Stock = booking.Stock;
                booking.BookingId = bookingId;
                

                // Get stock to check current Quantity
                var getStock = _context.Stocks.Single(s => s.Id == booking.StockId);
                
                if (booking.BookingQuantity > getStock.CurrentQuantity)
                {
                    // Do something
                    LoadStocks();
                    listOfOverload += getStock.Name + ", ";
                    check = "overload booking";
                }

                holdBookings.Add(booking);
            }

            if (check != null)
            {
                ViewBag.OverloadBooking = "The stock for " + listOfOverload + " is not enough";
                return View("Create", hold);
            }

            // Save the booking in the database if the booking is not overloaded
            foreach (var booking in holdBookings)
            {
                _context.Bookings.Add(booking);
            }

            _context.SaveChanges();

            var getId = ucBooking.ApplicationUser.Id;

            // TODO: Successful booking notification?
            return RedirectToAction("CustomerBooking", "Booking", new { id = getId });
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(string id, int bookingDateId)
        {
            // Check status if the guest try to edit from changing the link
            // TODO: other user can change the link to view others information. Restrict this.
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (user.Id != id)
            {
                return RedirectToAction("CustomerBooking", "Booking", new { id });
            }

            var getBookingStatus = _context.Bookings.First(b => b.BookingDateId == bookingDateId);

            if (getBookingStatus.BookingStatus == "Approved" || getBookingStatus.BookingStatus == "Rejected")
            {
                return RedirectToAction("CustomerBooking", "Booking", new {id});
            }

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

            return RedirectToAction("CustomerBooking", "Booking", new { id = getId });
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

        private static readonly Random Random = new Random();

        // Generate Random alphanumeric as BookingId
        public string GenerateRandomBookingId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            return new string (Enumerable.Repeat(chars, 4).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
