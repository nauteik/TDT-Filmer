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
    public class MovieController : Controller
    {
        private MovieWebsiteEntities db = new MovieWebsiteEntities();

        // GET: admin/Movie
        public ActionResult Index()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }
            return View(db.Movies.OrderByDescending(x => x.InitDate).ToList());
        }

        // GET: admin/Movie/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login", "AdminDefault");
            }
            ViewBag.Genres = db.Genres.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,Wallpaper,Score,Meta,C_ORDER,Type,Trailer,Summary,ReleaseDate,RunTime")] Movie movie, HttpPostedFileBase wallpaper)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload hình ảnh
                if (wallpaper != null)
                {
                    string filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + wallpaper.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/uploads"), filename);
                    wallpaper.SaveAs(path);
                    movie.Wallpaper = filename;
                }

                // Kiểm tra Type
                if (string.IsNullOrEmpty(movie.Type) || (movie.Type != "Theater" && movie.Type != "OnTV"))
                {
                    ModelState.AddModelError("Type", "Vui lòng chọn loại phim hợp lệ");
                    return View(movie);
                }

                // Xử lý các trường tự động
                movie.InitDate = DateTime.Now;
                movie.Hide = false;
                
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Genres = db.Genres.ToList();
            return View(movie);
        }

        // GET: admin/Movie/Edit/5
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

            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.Genres = db.Genres.ToList();
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,Wallpaper,Score,Meta,Hide,C_ORDER,Type,Trailer,Summary,ReleaseDate,RunTime,InitDate")] Movie movie, HttpPostedFileBase wallpaper)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý upload hình ảnh mới nếu có
                    if (wallpaper != null)
                    {
                        string filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + wallpaper.FileName;
                        string path = Path.Combine(Server.MapPath("~/Content/images/uploads"), filename);
                        wallpaper.SaveAs(path);
                        movie.Wallpaper = filename;
                    }

                    // Cập nhật Meta
                    movie.Meta = movie.Name.ToLower()
                        .Replace(" ", "-")
                        .Replace("đ", "d")
                        .Replace("'", "")
                        .Replace("\"", "");

                    db.Entry(movie).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật dữ liệu: " + ex.Message);
                }
            }

            ViewBag.Genres = db.Genres.ToList();
            return View(movie);
        }

        // GET: admin/Movie/Delete/5
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

            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);

            // Xóa file hình ảnh cũ nếu có
            if (!string.IsNullOrEmpty(movie.Wallpaper))
            {
                string oldPath = Path.Combine(Server.MapPath("~/Content/images/uploads"), movie.Wallpaper);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: admin/Movie/Details/5
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

            // Lấy thông tin phim kèm theo các thông tin liên quan
            Movie movie = db.Movies
                .Include(m => m.Genres)
                .Include(m => m.MovieCasts.Select(mc => mc.Celebrity))
                .Include(m => m.MovieReviews.Select(mr => mr.User))
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
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