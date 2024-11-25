using MovieWebsite.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MovieWebsite.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        MovieWebsiteEntities _db = new MovieWebsiteEntities();

        public ActionResult Index(string movieName = null, string category = null, string fromYear = null, string toYear = null, string sort = null, int page = 1, int pageSize = 12)
        {

            var query = _db.Movies.Where(m => m.Hide == false).AsQueryable();
            if (string.IsNullOrWhiteSpace(movieName) == false)
            {
                query = query.Where(m => m.Name.Contains(movieName));
            }
            if (string.IsNullOrWhiteSpace(category) == false)
            {
                query = query.Where(m => m.Genres.Any(g => g.Name == category));
            }
            if (string.IsNullOrWhiteSpace(sort) == false)
            {
                switch (sort)
                {
                    case "rating":
                        query = query.OrderByDescending(m => m.Score);
                        break;
                    case "year":
                        query = query.OrderByDescending(m => m.ReleaseDate.Value.Year);
                        break;
                }
            }
            if (int.TryParse(fromYear, out int from) && from >= 0)
            {
                query = query.Where(m => m.InitDate.Value.Year >= from);
            }
            if (int.TryParse(toYear, out int to) && to >= 0)
            {
                query = query.Where(m => m.InitDate.Value.Year <= to);
            }
            var movieList = query.ToList();
            int pageCount = (int)Math.Ceiling(movieList.Count / (double)pageSize);
            ViewBag.TotalMovies = movieList.Count;
            movieList = movieList
                            .OrderBy(m => m.Hide)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            ViewBag.Page = page;
            ViewBag.PageCount = pageCount;
            return View(movieList);
        }
        public ActionResult Detail(string meta)
        {
            var movie = _db.Movies.FirstOrDefault(m => m.Meta == meta);
            if (movie == null)
            {
                return View("NotFound");
            }
            ViewBag.Title = movie.Name;
            return View(movie);
        }
        public ActionResult MoviePagination(int currPage, int totalPages)
        {
            ViewBag.currPage = currPage;
            ViewBag.totalPages = totalPages;
            return PartialView();
        }
    }
}