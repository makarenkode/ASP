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
    [Authorize(Roles = "Admin")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class BossesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bosses
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var bosses = db.Bosses.OrderBy(b => b.Id);

            return View(bosses.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Bosses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boss boss = db.Bosses.Find(id);
            if (boss == null)
            {
                return HttpNotFound();
            }
            return View(boss);
        }

        // GET: Bosses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bosses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,SecondName")] Boss boss)
        {
            if (ModelState.IsValid)
            {
                db.Bosses.Add(boss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boss);
        }

        // GET: Bosses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boss boss = db.Bosses.Find(id);
            if (boss == null)
            {
                return HttpNotFound();
            }
            return View(boss);
        }

        // POST: Bosses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,SecondName")] Boss boss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boss);
        }

        // GET: Bosses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boss boss = db.Bosses.Find(id);
            if (boss == null)
            {
                return HttpNotFound();
            }
            return View(boss);
        }

        // POST: Bosses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boss boss = db.Bosses.Find(id);
            db.Bosses.Remove(boss);
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
