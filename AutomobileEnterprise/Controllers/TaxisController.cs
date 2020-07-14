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
    public class TaxisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Taxis
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var tax = db.Taxis.OrderBy(t => t.Id);
            return View(tax.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Taxis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // GET: Taxis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Taxis/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seats,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] Taxi taxi)
        {
            if (ModelState.IsValid)
            {
                db.Taxis.Add(taxi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxi);
        }

        // GET: Taxis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // POST: Taxis/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seats,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] Taxi taxi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxi);
        }

        // GET: Taxis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // POST: Taxis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Taxi taxi = db.Taxis.Find(id);
            db.Taxis.Remove(taxi);
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
