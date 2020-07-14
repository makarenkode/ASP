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
    [Authorize(Roles = "Admin,HR, Master")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class BrigadiersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Brigadiers
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var brigadiers = db.Brigadiers.Include(b => b.Brigade).OrderBy(b => b.FirstName);
            return View(brigadiers.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Brigadiers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigadier brigadier = db.Brigadiers.Find(id);
            if (brigadier == null)
            {
                return HttpNotFound();
            }
            return View(brigadier);
        }

        // GET: Brigadiers/Create
        public ActionResult Create()
        {
            ViewBag.BrigadeId = new SelectList(db.Brigades, "Id", "Id");
            return View();
        }

        // POST: Brigadiers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrigadeId,FirstName,SecondName")] Brigadier brigadier)
        {
            if (ModelState.IsValid)
            {
                db.Brigadiers.Add(brigadier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrigadeId = new SelectList(db.Brigades, "Id", "Id", brigadier.BrigadeId);
            return View(brigadier);
        }

        // GET: Brigadiers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigadier brigadier = db.Brigadiers.Find(id);
            if (brigadier == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrigadeId = new SelectList(db.Brigades, "Id", "Id", brigadier.BrigadeId);
            return View(brigadier);
        }

        // POST: Brigadiers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrigadeId,FirstName,SecondName")] Brigadier brigadier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brigadier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrigadeId = new SelectList(db.Brigades, "Id", "Id", brigadier.BrigadeId);
            return View(brigadier);
        }

        // GET: Brigadiers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigadier brigadier = db.Brigadiers.Find(id);
            if (brigadier == null)
            {
                return HttpNotFound();
            }
            return View(brigadier);
        }

        // POST: Brigadiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brigadier brigadier = db.Brigadiers.Find(id);
            db.Brigadiers.Remove(brigadier);
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
