using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomobileEnterprise.Models;
using AutomobileEnterprise.Models.Route;
using PagedList;

namespace AutomobileEnterprise.Controllers
{
    [Authorize(Roles = "Admin")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class DeparturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Departures
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var departures = db.Departures.Include(d => d.Car).OrderBy(d => d.DepartureId);
            return View(departures.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Departures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departures.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // GET: Departures/Create
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model");
            return View();
        }

        // POST: Departures/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartureId,CarId,Date")] Departure departure)
        {
            if (ModelState.IsValid)
            {
                db.Departures.Add(departure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model", departure.CarId);
            return View(departure);
        }

        // GET: Departures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departures.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model", departure.CarId);
            return View(departure);
        }

        // POST: Departures/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartureId,CarId,Date")] Departure departure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Model", departure.CarId);
            return View(departure);
        }

        // GET: Departures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departure departure = db.Departures.Find(id);
            if (departure == null)
            {
                return HttpNotFound();
            }
            return View(departure);
        }

        // POST: Departures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departure departure = db.Departures.Find(id);
            db.Departures.Remove(departure);
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
