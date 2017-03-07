using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blue_Ribbon.DAL;
using Blue_Ribbon.Models;

namespace Blue_Ribbon.Controllers
{
    public class ReviewLogController : Controller
    {
        private BRContext db = new BRContext();

        // GET: ReviewLog
        public ActionResult Index()
        {
            return View(db.ReviewLog.ToList());
        }

        // GET: ReviewLog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewLog reviewLog = db.ReviewLog.Find(id);
            if (reviewLog == null)
            {
                return HttpNotFound();
            }
            return View(reviewLog);
        }

        // GET: ReviewLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewLogId,ASIN,WebsiteAPIId,CustomerReviewed,AutomaticValidation,AdminReviewed,DisplayReview,Rating,CustomerId,ReviewSubject,ReviewBody,WouldBuyAgain,RecToFriend")] ReviewLog reviewLog)
        {
            if (ModelState.IsValid)
            {
                db.ReviewLog.Add(reviewLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reviewLog);
        }

        // GET: ReviewLog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewLog reviewLog = db.ReviewLog.Find(id);
            if (reviewLog == null)
            {
                return HttpNotFound();
            }
            return View(reviewLog);
        }

        // POST: ReviewLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewLogId,ASIN,WebsiteAPIId,CustomerReviewed,AutomaticValidation,AdminReviewed,DisplayReview,Rating,CustomerId,ReviewSubject,ReviewBody,WouldBuyAgain,RecToFriend")] ReviewLog reviewLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reviewLog);
        }

        // GET: ReviewLog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewLog reviewLog = db.ReviewLog.Find(id);
            if (reviewLog == null)
            {
                return HttpNotFound();
            }
            return View(reviewLog);
        }

        // POST: ReviewLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReviewLog reviewLog = db.ReviewLog.Find(id);
            db.ReviewLog.Remove(reviewLog);
            db.SaveChanges();
            return RedirectToAction("Index");
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
