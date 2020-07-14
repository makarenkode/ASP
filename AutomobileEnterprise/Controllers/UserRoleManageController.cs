using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using AutomobileEnterprise.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace AutomobileEnterprise.Controllers
{
    public class UserRoleManageController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        ApplicationDbContext db = new ApplicationDbContext();

        private int PageSize = 4;
        // GET: UserRoleManage
        public ActionResult Index(int? pageNum)
        {
            int numOfPage = pageNum ?? 1;
            var users = UserManager.Users.Include(u => u.Roles).OrderBy(u => u.UserName);
            return View(users.ToPagedList(numOfPage, PageSize));
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id.IsNullOrWhiteSpace())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Roles = await db.Roles.ToListAsync();
            ViewBag.UserRoles = await UserManager.GetRolesAsync(user.Id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,AccessFailedCount,UserName,LockoutEnabled,selectedRoles")] ApplicationUser user, string[] selectedRoles)
        {
            ViewBag.Roles = await db.Roles.ToListAsync();
            ViewBag.UserRoles = await UserManager.GetRolesAsync(user.Id);

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;

                var userRoles = await UserManager.GetRolesAsync(user.Id);
                var result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.ToArray());
                if (!result.Succeeded)
                {
                    return View(user);
                }
                if (selectedRoles != null)
                {
                    result = UserManager.AddToRoles(user.Id, selectedRoles);
                    if (!result.Succeeded)
                    {
                        return View(user);
                    }
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id.IsNullOrWhiteSpace())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Laboratories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            var result = await UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index");
        }

    }
}