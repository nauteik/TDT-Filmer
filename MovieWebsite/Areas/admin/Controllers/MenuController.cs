using MovieWebsite.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MovieWebsite.Areas.admin.Controllers
{
    public class MenuController : Controller
    {
        private MovieWebsiteEntities db = new MovieWebsiteEntities();

        // GET: admin/Menu
        public ActionResult Index()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }
            return View(db.Menus.ToList());
        }

        // GET: admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: admin/Menu/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Link,Meta,Hide,C_Order,InitDate")] Menu menu)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }

            if (ModelState.IsValid)
            {
                menu.InitDate = DateTime.Now;
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: admin/Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Link,Meta,Hide,C_Order,InitDate")] Menu menu)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }

            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật dữ liệu: " + ex.Message);
                }
            }
            return View(menu);
        }

        // GET: admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }

            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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