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
    public class WorkshopManagersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkshopManagers
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var workshopManagers = db.WorkshopManagers.Include(w => w.Workshop).OrderBy(w => w.FirstName);
            return View(workshopManagers.ToPagedList(numOfPage,sizeOfPage));
        }

        // GET: WorkshopManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkshopManager workshopManager = db.WorkshopManagers.Find(id);
            if (workshopManager == null)
            {
                return HttpNotFound();
            }
            return View(workshopManager);
        }

        // GET: WorkshopManagers/Create
        public ActionResult Create()
        {
            ViewBag.WorkshopId = new SelectList(db.Workshops, "Id", "Location");
            return View();
        }

        // POST: WorkshopManagers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WorkshopId,FirstName,SecondName")] WorkshopManager workshopManager)
        {
            if (ModelState.IsValid)
            {
                db.WorkshopManagers.Add(workshopManager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkshopId = new SelectList(db.Workshops, "Id", "Location", workshopManager.WorkshopId);
            return View(workshopManager);
        }

        // GET: WorkshopManagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkshopManager workshopManager = db.WorkshopManagers.Find(id);
            if (workshopManager == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkshopId = new SelectList(db.Workshops, "Id", "Location", workshopManager.WorkshopId);
            return View(workshopManager);
        }

        // POST: WorkshopManagers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WorkshopId,FirstName,SecondName")] WorkshopManager workshopManager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workshopManager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkshopId = new SelectList(db.Workshops, "Id", "Location", workshopManager.WorkshopId);
            return View(workshopManager);
        }

        // GET: WorkshopManagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkshopManager workshopManager = db.WorkshopManagers.Find(id);
            if (workshopManager == null)
            {
                return HttpNotFound();
            }
            return View(workshopManager);
        }

        // POST: WorkshopManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkshopManager workshopManager = db.WorkshopManagers.Find(id);
            db.WorkshopManagers.Remove(workshopManager);
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
