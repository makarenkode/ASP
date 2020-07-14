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
    public class TractorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tractors
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var tractors = db.Tractors.OrderBy(t => t.Id);
            return View(tractors.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Tractors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tractor tractor = db.Tractors.Find(id);
            if (tractor == null)
            {
                return HttpNotFound();
            }
            return View(tractor);
        }

        // GET: Tractors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tractors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarringCapacity,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] Tractor tractor)
        {
            if (ModelState.IsValid)
            {
                db.Tractors.Add(tractor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tractor);
        }

        // GET: Tractors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tractor tractor = db.Tractors.Find(id);
            if (tractor == null)
            {
                return HttpNotFound();
            }
            return View(tractor);
        }

        // POST: Tractors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarringCapacity,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] Tractor tractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tractor);
        }

        // GET: Tractors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tractor tractor = db.Tractors.Find(id);
            if (tractor == null)
            {
                return HttpNotFound();
            }
            return View(tractor);
        }

        // POST: Tractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tractor tractor = db.Tractors.Find(id);
            db.Tractors.Remove(tractor);
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
