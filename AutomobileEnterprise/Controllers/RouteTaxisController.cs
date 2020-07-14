using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomobileEnterprise.Models;
using AutomobileEnterprise.Models.Cars;
using PagedList;

namespace AutomobileEnterprise.Controllers
{
    [Authorize(Roles = "Admin")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class RouteTaxisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RouteTaxis
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var routeTaxis = db.RouteTaxis.Include(r => r.Route).OrderBy(r => r.Id);
            return View(routeTaxis.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: RouteTaxis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteTaxi routeTaxi = db.RouteTaxis.Find(id);
            if (routeTaxi == null)
            {
                return HttpNotFound();
            }
            return View(routeTaxi);
        }

        // GET: RouteTaxis/Create
        public ActionResult Create()
        {
            ViewBag.RouteId = new SelectList(db.Routes, "Id", "Name");
            return View();
        }

        // POST: RouteTaxis/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seats,RouteId,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] RouteTaxi routeTaxi)
        {
            if (ModelState.IsValid)
            {
                db.RouteTaxis.Add(routeTaxi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RouteId = new SelectList(db.Routes, "Id", "Name", routeTaxi.RouteId);
            return View(routeTaxi);
        }

        // GET: RouteTaxis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteTaxi routeTaxi = db.RouteTaxis.Find(id);
            if (routeTaxi == null)
            {
                return HttpNotFound();
            }
            ViewBag.RouteId = new SelectList(db.Routes, "Id", "Name", routeTaxi.RouteId);
            return View(routeTaxi);
        }

        // POST: RouteTaxis/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seats,RouteId,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] RouteTaxi routeTaxi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routeTaxi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RouteId = new SelectList(db.Routes, "Id", "Name", routeTaxi.RouteId);
            return View(routeTaxi);
        }

        // GET: RouteTaxis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteTaxi routeTaxi = db.RouteTaxis.Find(id);
            if (routeTaxi == null)
            {
                return HttpNotFound();
            }
            return View(routeTaxi);
        }

        // POST: RouteTaxis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RouteTaxi routeTaxi = db.RouteTaxis.Find(id);
            db.RouteTaxis.Remove(routeTaxi);
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
