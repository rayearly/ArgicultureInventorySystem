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
    [Authorize(Roles = RoleName.CanManageBookings)]
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

        [AllowAnonymous]
        public ActionResult RedirectUserHome(string id)
        {
            // TODO: Create Admin Menu
            return User.IsInRole(RoleName.CanManageBookings) ? AdminHomePage() : CustomerBooking(id);
        }

        // DISPLAY: Menu for Admin
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

        [AllowAnonymous]
        public ActionResult RedirectUserBooking(string id)
        {
            return User.IsInRole(RoleName.CanManageBookings) ? AdminBooking(id) : CustomerBooking(id);
        }

        // GET: Booking for Specific Customer
        [AllowAnonymous]
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
                Bookings = getSpecificBooking.DistinctBy(b => b.BookingDateId).OrderByDescending(b => b.BookingDateId),
                BookingDates = getBookingDates.Distinct(),
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == id)
            };

            return View("CustBookingList", viewModel);

        }

        // Admin Booking : same as Customer except redirected to another page? Fix this
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
                Bookings = getSpecificBooking.DistinctBy(b => b.BookingDateId).OrderByDescending(b => b.BookingDateId),
                BookingDates = getBookingDates.Distinct(),
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == id)
            };

            return View("AdminBookingList", viewModel);
        }

        // GET: Booking List for Admin (Show list of Customer)
        public ActionResult AllBookingList()
        {
            var uc = _context.Users.ToList();

            return View("Index", uc);
        }

        #region Get the different type of Booking status? - can be in one function TODO:

        // GET: Get Unapproved Bookings and display it in a page to be approved by admin
        public ActionResult UnApprovedBookingList()
        {
            // Get the unapproved bookings
            var unapproved = _context.Bookings.Where(b => b.BookingStatus == "In Process").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = unapproved.DistinctBy(b => b.BookingDateId).OrderByDescending(b => b.BookingDateId)
            };

            return View("UnapprovedBookingList", viewModel);
        }

        // GET: Get Approved Bookings and display it in a page to be viewed/rejected by admin
        public ActionResult ApprovedBookingList()
        {
            // Get the approved bookings
            var approved = _context.Bookings.Where(b => b.BookingStatus == "Approved").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = approved.DistinctBy(b => b.BookingDateId).OrderByDescending(b => b.BookingDateId)
            };

            return View("ApprovedBookingList", viewModel);
        }

        // GET: Get rejected Bookings and display it in a page to be viewed/approved by admin
        public ActionResult RejectedBookingList()
        {
            // Get the unapproved bookings
            var rejected = _context.Bookings.Where(b => b.BookingStatus == "Rejected").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = rejected.DistinctBy(b => b.BookingDateId).OrderByDescending(b => b.BookingDateId)
            };

            return View("RejectedBookingList", viewModel);
        }

        // GET: Get returned Bookings and display it in a page to be viewed by admin
        public ActionResult ReturnedBookingList()
        {
            // Get the unapproved bookings
            var rejected = _context.Bookings.Where(b => b.BookingStatus == "Returned").ToList();

            // This is the list of booking not sorted by booking date
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = rejected.DistinctBy(b => b.BookingDateId).OrderByDescending(b => b.BookingDateId)
            };

            return View("ReturnedBookingList", viewModel);
        }

        #endregion

        #region Update different type of booking status - can be refactored?

        [HttpPut]
        public ActionResult ApproveBooking(int bookingId)
        {
            var booking = _context.Bookings.Where(b => b.BookingDateId == bookingId);
            var stock = _context.Stocks.ToList();

            // Check if the booking is overload or not by message exist
            string check = null;

            // List the stock that had book overloaded
            string stocklist = null;

            foreach (var book in booking)
            {
                foreach (var s in stock)
                {
                    if (s.Id == book.StockId)
                    {
                        // Substract current quantity with quantity booked
                        // TODO: check if the value is negative/zero, dont make approval
                        var currentQty = s.CurrentQuantity - book.BookingQuantity;

                        if (currentQty < 0)
                        {
                            check = "booking overload";
                            stocklist += s.Name + ", ";
                        }

                        s.CurrentQuantity = currentQty;
                    }
                }

                // Set approval (string) into Approved
                book.BookingStatus = "Approved";
            }

            if (check != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "The stock for " + stocklist + "is not enough");
            }

            _context.SaveChanges();

            // If update success then send code OK.
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // TODO: Write Reject reason / comment
        [HttpPut]
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

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPut]
        public ActionResult ReturnBooking(int bookingId)
        {
            var booking = _context.Bookings.Where(b => b.BookingDateId == bookingId);
            var stock = _context.Stocks.ToList();

            foreach (var book in booking)
            {
                foreach (var s in stock)
                {
                    if (s.Id != book.StockId) continue;

                    // Add current quantity with quantity booked (returned) if it is Tool type?
                    // Tool Stock Type Id = 1
                    if (s.TypeId != 1) continue;
                    var currentQty = s.CurrentQuantity + book.BookingQuantity;
                    s.CurrentQuantity = currentQty;
                }

                // Set approval into returned
                book.BookingStatus = "Returned";
            }

            _context.SaveChanges();

            // If update success then send code OK.
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        // GET: Booking/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var booking = _context.Bookings.Where(b => b.BookingDateId == id);

            var stock = _context.Stocks.ToList();

            var viewModel = new UcBookingStockViewModel
            {
                Bookings = booking,
                Stocks = stock
            };

            // If user is not admin then check identity. Admin can freely view any user booking details
            if (User.IsInRole(RoleName.CanManageBookings)) return View("Details", viewModel);
            {
                var getBookingUser = _context.Bookings.First(b => b.BookingDateId == id);

                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                // Check if current user is the user that using this system
                if (user.Id != getBookingUser.UserId)
                {
                    return RedirectToAction("CustomerBooking", "Booking", new { user.Id });
                }

                return View("CustBookingDetails", viewModel);
            }

        }

        // GET: Booking/Create
        [AllowAnonymous]
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

        // POST: Booking/Create
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(UcBookingStockViewModel ucBooking)
        {
            // TODO: Check if the stock selected is the same as previous one in the list or not. If same, deny booking.
            
            var hold = ucBooking;

            if (!ModelState.IsValid || ucBooking.Bookings == null)
            {
                LoadStocks();
                return View("Create", ucBooking);
            }

            string overloadBooking = null;
            string zeroBooking = null;
            string listOfOverload = null;

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
                
                // Check if the booked quantity is more than available quantity. If yes, it is overloaded.
                if (booking.BookingQuantity > getStock.CurrentQuantity)
                {
                    listOfOverload += getStock.Name + ", ";
                    overloadBooking = "overload booking";
                }

                if (booking.BookingQuantity <= 0)
                {
                    zeroBooking = "zero booking";
                }

                _context.Bookings.Add(booking);
            }

            if (overloadBooking != null)
            {
                ViewBag.OverloadBooking = "The stock for " + listOfOverload + " is not enough";
                LoadStocks();
                return View("Create", hold);
            }

            if (zeroBooking != null)
            {
                ViewBag.ZeroBooking = "Number of item must be at least 1.";
                LoadStocks();
                return View("Create", hold);
            }

            var getId = ucBooking.ApplicationUser.Id;

            // Detect the exception (eg; duplicate PK)
            try
            {
                // Save the booking in the database if the booking is not overloaded
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(@"{0} Exception caught.", e);
                ViewBag.Error = "Booking cannot be duplicated";
                LoadStocks();
                return View("Create", hold);
            }
            
            // TODO: Successful booking notification?
            return RedirectToAction(User.IsInRole(RoleName.CanManageBookings) ? "AdminBooking" : "CustomerBooking", "Booking", new { id = getId });
        }

        // GET: Booking/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(string id, int bookingDateId)
        {
            // Check status if the guest try to edit from changing the link
            if (!User.IsInRole(RoleName.CanManageBookings))
            {
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                if (user.Id != id)
                {
                    return RedirectToAction("CustomerBooking", "Booking", new { id });
                }

                var getBookingStatus = _context.Bookings.First(b => b.BookingDateId == bookingDateId);

                // If booking is already approved/rejected/returned, then redirect to user booking page
                if (getBookingStatus.BookingStatus == "Approved" || getBookingStatus.BookingStatus == "Rejected" || getBookingStatus.BookingStatus == "Returned")
                {
                    return RedirectToAction("CustomerBooking", "Booking", new { id });
                }
            }
            
            LoadStocks();

            // Get the universityCommunity information by passed Id
            var uc = _context.Users.SingleOrDefault(u => u.Id == id);

            // Get the whole list of bookings
            var getBookings = uc.Bookings.ToList();

            var book = new List<Booking>(getBookings.Where(b => b.BookingDateId == bookingDateId));
            
            var viewModel = new UcBookingStockViewModel
            {
                Bookings = book,
                Stocks = _context.Stocks.ToList(),
                BookingDate = _context.BookingDates.Single(b => b.Id == bookingDateId),
                ApplicationUser = uc
            };

            return View(viewModel);
        }

        // POST: Booking/Edit/5
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Edit(string id, UcBookingStockViewModel ucBooking)
        {
            var hold = ucBooking;

            var getBooking = _context.Bookings.ToList();

            // Get Bookings specific to the customer
            var getSpecificBooking = getBooking.Where(b => b.ApplicationUser.Id == ucBooking.ApplicationUser.Id).ToList();

            var getSpecificBooking2 = _context.Bookings.Where(b => b.BookingDateId == ucBooking.BookingDate.Id);

            var viewModel = new UcBookingStockViewModel
            {
                ApplicationUser = _context.Users.SingleOrDefault(u => u.Id == ucBooking.ApplicationUser.Id),
                Bookings = getSpecificBooking2,
                BookingDate = ucBooking.BookingDate
            };

            if (!ModelState.IsValid || ucBooking.Bookings == null)
            {
                LoadStocks();
                return View("Edit", viewModel);
            }

            string check = null;
            string listOfOverload = null;
            string zeroBooking = null;

            foreach (var booking in ucBooking.Bookings)
            {
                if (booking.BookingDateId == 0)
                {
                    //Insert if bookingId 0
                    booking.UserId = ucBooking.ApplicationUser.Id;
                    booking.Stock = booking.Stock;
                    booking.BookingDateId = ucBooking.BookingDate.Id;
                    booking.BookingId = ucBooking.Bookings.First().BookingId;

                    // Get stock to check current Quantity
                    var getStock = _context.Stocks.Single(s => s.Id == booking.StockId);

                    // Check if the booked quantity is more than available quantity. If yes, it is overloaded.
                    if (booking.BookingQuantity > getStock.CurrentQuantity)
                    {
                        listOfOverload += getStock.Name + ", ";
                        check = "overload booking";
                    }

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
                    bInDb.BookingId = booking.BookingId;

                    // Get stock to check current Quantity
                    var getStock = _context.Stocks.Single(s => s.Id == booking.StockId);

                    // Check if the booked quantity is more than available quantity. If yes, it is overloaded.
                    if (booking.BookingQuantity > getStock.CurrentQuantity)
                    {
                        listOfOverload += getStock.Name + ", ";
                        check = "overload booking";
                    }
                    
                    // If item booked entered is less than 0 or equal to
                    if (booking.BookingQuantity <= 0)
                    {
                        zeroBooking = "zero booking";
                    }

                }
            }

            if (check != null)
            {
                ViewBag.OverloadBooking = "The stock for " + listOfOverload + " is not enough";
                LoadStocks();
                return View("Edit", hold);
            }

            if (zeroBooking != null)
            {
                ViewBag.ZeroBooking = "Number of item must be at least 1.";
                LoadStocks();
                return View("Edit", hold);
            }

            var getId = ucBooking.ApplicationUser.Id;

            //_context.SaveChanges();

            // Detect the exception (eg; duplicate PK)
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(@"{0} Exception caught.", e);
                ViewBag.Error = "Booking cannot be duplicated";
                LoadStocks();
                return View("Edit", hold);
            }

            // TODO: Successful booking notification?
            return RedirectToAction(User.IsInRole(RoleName.CanManageBookings) ? "AdminBooking" : "CustomerBooking", "Booking", new { id = getId });
        }

        // GET: Booking/Delete/5
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpDelete]
        public ActionResult Delete(int? id, int? stockId, string ucId)
        {
            // If the deleted row is empty without selection - allow it
            if (id == 0 || stockId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
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

        // TODO: Delete whole booking. Current delete is for deleting one row in a booking.
        [HttpDelete]
        public ActionResult DeleteByBookingDateId(int bookingDateId)
        {
            // Get the bookings that have the same bookingDateId to be deleted
            var bookingToDelete = _context.Bookings.Where(b => b.BookingDateId == bookingDateId);

            if (bookingToDelete.Any())
            {
                foreach (var booking in bookingToDelete)
                {
                    // Delete each of the bookings
                    _context.Bookings.Remove(booking);
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

        #region LoadStock - Get the stock into the ViewBag to be used in the PartialViews
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult BookingPartialResult()
        {
            LoadStocks();
            return PartialView("_BookingPartial", new Booking());
        }

        private static readonly Random Random = new Random();

        // Generate Random alphanumeric as BookingId
        [AllowAnonymous]
        public string GenerateRandomBookingId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            return new string (Enumerable.Repeat(chars, 4).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
