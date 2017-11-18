using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Digital_Planner.Models;

namespace Digital_Planner.Controllers
{
    public class DaysController : Controller
    {
        private calendarEntities db = new calendarEntities();

        // GET: Days
        public ActionResult Index()
        {
            var days = db.Days.Include(d => d.User);
            return View(days.ToList());
        }

        // GET: Days/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // GET: Days/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoursAvailable,Date,UserID")] Day day, int recurrence)
        {

            if (ModelState.IsValid)
            {
                db.Days.Add(day);
                db.SaveChanges();
                
                //Keep those rascals under control!
                if(recurrence > 24)
                {
                    recurrence = 24;
                }
                var day_to_use = day.Date;
                while(recurrence > 0)
                {
                    var re_day = new Day();
                    day_to_use = day_to_use.AddDays(7);

                    re_day.Date = day_to_use;
                    re_day.HoursAvailable = day.HoursAvailable;
                    re_day.UserID = day.UserID;

                    db.Days.Add(re_day);
                    db.SaveChanges();

                    recurrence--;
                }

                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", day.UserID);
            return View(day);
        }

        // GET: Days/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", day.UserID);
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoursAvailable,Date,UserID")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Entry(day).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", day.UserID);
            return View(day);
        }

        // GET: Days/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Day day = db.Days.Find(id);
            db.Days.Remove(day);
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
