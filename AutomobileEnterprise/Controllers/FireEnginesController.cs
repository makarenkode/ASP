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
    public class FireEnginesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FireEngines
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var fire = db.FireEngines.OrderBy(f => f.Id);
            return View(fire.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: FireEngines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FireEngine fireEngine = db.FireEngines.Find(id);
            if (fireEngine == null)
            {
                return HttpNotFound();
            }
            return View(fireEngine);
        }

        // GET: FireEngines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FireEngines/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TankVolume,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] FireEngine fireEngine)
        {
            if (ModelState.IsValid)
            {
                db.FireEngines.Add(fireEngine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fireEngine);
        }

        // GET: FireEngines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FireEngine fireEngine = db.FireEngines.Find(id);
            if (fireEngine == null)
            {
                return HttpNotFound();
            }
            return View(fireEngine);
        }

        // POST: FireEngines/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TankVolume,Model,Mileage,PurchaseDate,WriteOffDate,Cat")] FireEngine fireEngine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fireEngine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fireEngine);
        }

        // GET: FireEngines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FireEngine fireEngine = db.FireEngines.Find(id);
            if (fireEngine == null)
            {
                return HttpNotFound();
            }
            return View(fireEngine);
        }

        // POST: FireEngines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FireEngine fireEngine = db.FireEngines.Find(id);
            db.FireEngines.Remove(fireEngine);
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
