using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomobileEnterprise.Models;
using AutomobileEnterprise.Models.Buildings;
using PagedList;

namespace AutomobileEnterprise.Controllers
{
    [Authorize(Roles = "Admin")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class WorkshopsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Workshops
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var workshops = db.Workshops.OrderBy(w => w.Id);
            return View(workshops.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Workshops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }

        // GET: Workshops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workshops/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Location")] Workshop workshop)
        {
            if (ModelState.IsValid)
            {
                db.Workshops.Add(workshop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workshop);
        }

        // GET: Workshops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }

        // POST: Workshops/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location")] Workshop workshop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workshop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workshop);
        }

        // GET: Workshops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }

        // POST: Workshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workshop workshop = db.Workshops.Find(id);
            db.Workshops.Remove(workshop);
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
