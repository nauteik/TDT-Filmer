using MovieWebsite.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MovieWebsite.Controllers
{
    public class CelebController : Controller
    {
        MovieWebsiteEntities _db = new MovieWebsiteEntities();
        // GET: Celeb
        public ActionResult Detail(string meta)
        {
            var celeb = _db.Celebrities.FirstOrDefault(c => c.Meta == meta);
            if (celeb == null)
                return View("NotFound");
            return View(celeb);

        }
        public ActionResult CelebList(string name = null, int page = 1, int pageSize = 12)
        {
            var query = _db.Celebrities.Where(m => m.Hide == false).AsQueryable();
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                query = query.Where(m => m.Name.Contains(name));
            }
            var celebList = query.ToList();
            int pageCount = (int)Math.Ceiling(celebList.Count / (double)pageSize);
            ViewBag.TotalCeleb = celebList.Count;
            celebList = celebList
            .OrderBy(m => m.Hide)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            ViewBag.Page = page;
            ViewBag.PageCount = pageCount;

            return View(celebList);
        }
        public ActionResult Cast(int celebId)
        {
            var casts = (from cast in _db.MovieCasts
                         join movie in _db.Movies on cast.MovieId equals movie.Id
                         where cast.CelebId == celebId
                         select new CastViewModel
                         {
                             MovieName = movie.Name,
                             MovieImage = movie.Wallpaper,
                             MovieMeta = movie.Meta,
                             Role = cast.Role,
                             Year = movie.ReleaseDate.Value.Year
                         }).ToList();
            return PartialView(casts);
        }
        public ActionResult CelebPagination(int currPage, int totalPages)
        {
            ViewBag.currPage = currPage;
            ViewBag.totalPages = totalPages;
            return PartialView();
        }
    }
}