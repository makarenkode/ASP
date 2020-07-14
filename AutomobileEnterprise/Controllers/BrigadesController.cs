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
    [Authorize(Roles = "Admin, Master")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class BrigadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Brigades
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var brigades = db.Brigades.Include(b => b.Master).OrderBy(b => b.Id);
            return View(brigades.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Brigades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigade brigade = db.Brigades.Find(id);
            if (brigade == null)
            {
                return HttpNotFound();
            }
            return View(brigade);
        }

        // GET: Brigades/Create
        public ActionResult Create()
        {
            ViewBag.MasterId = new SelectList(db.Masters, "Id", "FirstName");
            return View();
        }

        // POST: Brigades/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MasterId")] Brigade brigade)
        {
            if (ModelState.IsValid)
            {
                db.Brigades.Add(brigade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterId = new SelectList(db.Masters, "Id", "FirstName", brigade.MasterId);
            return View(brigade);
        }

        // GET: Brigades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigade brigade = db.Brigades.Find(id);
            if (brigade == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterId = new SelectList(db.Masters, "Id", "FirstName", brigade.MasterId);
            return View(brigade);
        }

        // POST: Brigades/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MasterId")] Brigade brigade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brigade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterId = new SelectList(db.Masters, "Id", "FirstName", brigade.MasterId);
            return View(brigade);
        }

        // GET: Brigades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brigade brigade = db.Brigades.Find(id);
            if (brigade == null)
            {
                return HttpNotFound();
            }
            return View(brigade);
        }

        // POST: Brigades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brigade brigade = db.Brigades.Find(id);
            db.Brigades.Remove(brigade);
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
