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
    [Authorize(Roles = "Admin,HR")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class MastersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Masters
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var masters = db.Masters.Include(m => m.WorkshopManager).OrderBy(m => m.FirstName);
            return View(masters.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Masters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Masters.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // GET: Masters/Create
        public ActionResult Create()
        {
            ViewBag.WorkshopManagerId = new SelectList(db.WorkshopManagers, "Id", "FirstName");
            return View();
        }

        // POST: Masters/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WorkshopManagerId,FirstName,SecondName")] Master master)
        {
            if (ModelState.IsValid)
            {
                db.Masters.Add(master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkshopManagerId = new SelectList(db.WorkshopManagers, "Id", "FirstName", master.WorkshopManagerId);
            return View(master);
        }

        // GET: Masters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Masters.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkshopManagerId = new SelectList(db.WorkshopManagers, "Id", "FirstName", master.WorkshopManagerId);
            return View(master);
        }

        // POST: Masters/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WorkshopManagerId,FirstName,SecondName")] Master master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkshopManagerId = new SelectList(db.WorkshopManagers, "Id", "FirstName", master.WorkshopManagerId);
            return View(master);
        }

        // GET: Masters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Masters.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: Masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master master = db.Masters.Find(id);
            db.Masters.Remove(master);
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
