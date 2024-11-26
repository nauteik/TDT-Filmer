using MovieWebsite.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieWebsite.Areas.admin.Controllers
{
    public class NewController : Controller
    {
        private MovieWebsiteEntities db = new MovieWebsiteEntities();

        // GET: admin/New
        public ActionResult Index()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }
            return View(db.News.OrderByDescending(x => x.InitDate).ToList());
        }

        // GET: admin/New/Create
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
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Title,Image,Content,Meta,C_ORDER")] News news, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    string filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/uploads"), filename);
                    image.SaveAs(path);
                    news.Image = filename;
                }

                news.Meta = news.Title.ToLower()
                    .Replace(" ", "-")
                    .Replace("đ", "d")
                    .Replace("'", "")
                    .Replace("\"", "");
                news.Hide = false;
                news.InitDate = DateTime.Now;

                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: admin/New/Edit/5
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
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,Image,Content,Meta,Hide,C_ORDER,InitDate")] News news, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        string filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        string path = Path.Combine(Server.MapPath("~/Content/images/uploads"), filename);
                        image.SaveAs(path);
                        news.Image = filename;
                    }

                    news.Meta = news.Title.ToLower()
                        .Replace(" ", "-")
                        .Replace("đ", "d")
                        .Replace("'", "")
                        .Replace("\"", "");

                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật dữ liệu: " + ex.Message);
                }
            }
            return View(news);
        }

        // GET: admin/New/Details/5
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

            News news = db.News
                .Include(n => n.NewComments.Select(nc => nc.User))
                .FirstOrDefault(n => n.Id == id);

            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        // GET: admin/New/Delete/5
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

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);

            // Xóa file hình ảnh cũ nếu có
            if (!string.IsNullOrEmpty(news.Image))
            {
                string oldPath = Path.Combine(Server.MapPath("~/Content/images/uploads"), news.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            db.News.Remove(news);
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