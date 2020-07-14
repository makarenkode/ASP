using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomobileEnterprise.Models;
using AutomobileEnterprise.Models.Route;
using PagedList;

namespace AutomobileEnterprise.Controllers
{
    [Authorize(Roles = "Admin")]
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class RoutesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Routes
        public ActionResult Index(int? pageNum)
        {
            int sizeOfPage = 4;
            int numOfPage = (pageNum ?? 1);
            var routes = db.Routes.OrderBy(r => r.Id);
            return View(routes.ToPagedList(numOfPage, sizeOfPage));
        }

        // GET: Routes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // GET: Routes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Distance")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(routes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(routes);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: Routes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Distance")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(routes);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = db.Routes.Find(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Routes routes = db.Routes.Find(id);
            db.Routes.Remove(routes);
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
