using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomobileEnterprise.Models;
using AutomobileEnterprise.Models.Staffs;
using PagedList;

namespace AutomobileEnterprise.Controllers
{
    [Authorize(Roles = "Admin, HR")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class ServiceWorkersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceWorkers
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var ser = db.ServiceWorkers.OrderBy(s => s.FirstName);
            return View(ser.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: ServiceWorkers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceWorker serviceWorker = db.ServiceWorkers.Find(id);
            if (serviceWorker == null)
            {
                return HttpNotFound();
            }
            return View(serviceWorker);
        }

        // GET: ServiceWorkers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceWorkers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Specialization,FirstName,SecondName")] ServiceWorker serviceWorker)
        {
            if (ModelState.IsValid)
            {
                db.ServiceWorkers.Add(serviceWorker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceWorker);
        }

        // GET: ServiceWorkers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceWorker serviceWorker = db.ServiceWorkers.Find(id);
            if (serviceWorker == null)
            {
                return HttpNotFound();
            }
            return View(serviceWorker);
        }

        // POST: ServiceWorkers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Specialization,FirstName,SecondName")] ServiceWorker serviceWorker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceWorker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceWorker);
        }

        // GET: ServiceWorkers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceWorker serviceWorker = db.ServiceWorkers.Find(id);
            if (serviceWorker == null)
            {
                return HttpNotFound();
            }
            return View(serviceWorker);
        }

        // POST: ServiceWorkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceWorker serviceWorker = db.ServiceWorkers.Find(id);
            db.ServiceWorkers.Remove(serviceWorker);
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
