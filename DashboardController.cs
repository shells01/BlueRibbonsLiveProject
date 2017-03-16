using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blue_Ribbon.Models;
using Blue_Ribbon.ViewModels;
using Blue_Ribbon.DAL;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System;
using Blue_Ribbon.AmazonAPI;

namespace Blue_Ribbon.Controllers
{

    public class DashboardController : Controller
    {
        ApplicationDbContext dba = new ApplicationDbContext();
        private BRContext db = new BRContext();

        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.Name;

            Customer selectedCust = (from cust in db.Customers
                                     where cust.CustomerID.Equals(userId)
                                     select cust).First();

            CheckForCompletedReviews check = new CheckForCompletedReviews(selectedCust);
            check.Check();
            selectedCust.LastReviewCheck = DateTime.Now;
            db.Entry(selectedCust).State = EntityState.Modified;
            db.SaveChanges();


            return View(selectedCust);
        }

        // GET: Edit
        [Authorize]
        public ActionResult Edit(int? id)
        {
            string userId = User.Identity.Name;

            Customer selectedCust = (from cust in db.Customers
                                     where cust.CustomerID.Equals(userId)
                                     select cust).First();

            CheckForCompletedReviews check = new CheckForCompletedReviews(selectedCust);
            check.Check();
            selectedCust.LastReviewCheck = DateTime.Now;
            db.Entry(selectedCust).State = EntityState.Modified;
            db.SaveChanges();


            return View(selectedCust);
        }

        // NEW: Partial View for ReviewLogViewModel items-to-be-reviewed on customer dashboard (Tab 4, BRR Deals)
        public ActionResult ItemsToReview()
        {
            
            string user = User.Identity.Name;
            
            string customerEmail = (from cust in db.Customers
                                    where cust.CustomerID.Equals(user)
                                    select cust.Email).First();

            List<ReviewLog> Logs = (from log in db.ReviewLog
                                              where log.Email.Equals(customerEmail) && log.CustomerReviewed == false
                                              select log).ToList();

            List<Deal> Deals = db.Deal.ToList();

            List<ReviewLogViewModel> ItemsToReview = new List<ReviewLogViewModel>();

            for (var i = 0; i < Logs.Count; i++)
            {
                var vm = new ReviewLogViewModel(Deals.Where(a => a.ASIN == Logs[i].ASIN).FirstOrDefault(), Logs[i]);
                ItemsToReview.Add(vm);
            }

            return PartialView(ItemsToReview); 
        }

        // NEW: Partial View for ReviewLogViewModel reviews on customer dashboard (Tab 5, BRR Reviews)
        public ActionResult InHouseReviews()
        {
            string user = User.Identity.Name;

            string customerEmail = (from cust in db.Customers
                                    where cust.CustomerID.Equals(user)
                                    select cust.Email).First();

            List<ReviewLog> Logs = (from log in db.ReviewLog
                                    where log.Email.Equals(customerEmail) && log.CustomerReviewed == true
                                    select log).ToList();

            List<Deal> Deals = db.Deal.ToList();

            List<ReviewLogViewModel> InHouseReviews = new List<ReviewLogViewModel>();

            for (var i = 0; i < Logs.Count; i++)
            {
                var vm = new ReviewLogViewModel(Deals.Where(a => a.ASIN == Logs[i].ASIN).FirstOrDefault(), Logs[i]);
                InHouseReviews.Add(vm);
            }

            return PartialView(InHouseReviews);
        }

        public ActionResult Carousel()
        {
            
            List<Campaign> products = (from camp in db.Campaigns
                                       where camp.OpenCampaign == true
                                       select camp).ToList();

            return PartialView(products);
        }

        public ActionResult GetFormPartial(string email, string name)
        {
            ContactFormViewModel newMessage = new ContactFormViewModel();
            newMessage.Email = email;
            newMessage.Name = name;
            newMessage.LoggedIn = User.Identity.IsAuthenticated;
            newMessage.AmazonID = User.Identity.Name;

            return PartialView("_ContactFormPartial",newMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ContactForm(ContactFormViewModel newmessage)
        {
            var response = newmessage.SendForm();

            var data = new
            {
                message = "Message Sent!"
            };

            return Json(data);
        }

        public ActionResult Welcome(string message)
        {
            if(message == "confirmmail")
            {
                ViewBag.Message = "Please check your email and confirm your account. You must be confirmed "
                                + "before you can log in start shopping!";
            }
            return View();
        }

        [HttpPost]
        public JsonResult ReviewCheckForm(int reviewid, string reviewlink)
        {

            if (String.IsNullOrEmpty(reviewlink))
            {
                var dataFail = new
                {
                    success = false,
                    message = "Please enter a link for us to check."
                };

                return Json(dataFail);
            }

            CheckReviewByUrl reviewCheck = new CheckReviewByUrl(reviewid, reviewlink);

            var data = new
            {
                success = reviewCheck.ReviewConfirmed,
                message = reviewCheck.Response
            };

            return Json(data);
        }

        [HttpPost]
        public JsonResult ReviewCheckManualRequest(int reviewid, string reviewlink)
        {

            if (String.IsNullOrEmpty(reviewlink))
            {
                var dataFail = new
                {
                    success = false,
                    message = "Please enter a link for us to check."
                };

                return Json(dataFail);
            }

            CheckReviewByUrl reviewCheck = new CheckReviewByUrl(reviewid, reviewlink,true);

            var data = new
            {
                success = reviewCheck.RequestLogged,
                message = reviewCheck.Response
            };

            return Json(data);
        }

        [HttpPost]
        public JsonResult DeleteRequest(int requestid)
        {

            string thisUser = User.Identity.Name;
            bool wasSuccessful = false;

            ItemRequest req = db.ItemRequests.Find(requestid);
            if(req.CustomerID == thisUser)
            {
                db.ItemRequests.Remove(req);
                db.SaveChanges();
                wasSuccessful = true;
            }

            var data = new
            {
                success = wasSuccessful
            };

            return Json(data);
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