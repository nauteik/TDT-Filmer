using MovieWebsite.Models;
using System.Linq;
using System.Web.Mvc;
namespace MovieWebsite.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        MovieWebsiteEntities _db = new MovieWebsiteEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenu()
        {
            var viewList = _db.Menus
                            .Where(menu => menu.Hide == false)
                            .OrderBy(menu => menu.C_Order)
                            .ToList();
            return PartialView(viewList);
        }
        public ActionResult GetTheaterMovie()
        {
            var viewList = _db.Movies
                            .Where(movie => movie.Hide == false && movie.Type == "Theater")
                            .OrderBy(movie => movie.C_ORDER)
                            .Take(12) // Limit by 12 movie for showing in index page
                            .ToList();

            return PartialView(viewList);
        }
        public ActionResult GetSlider()
        {
            var viewList = _db.Movies
                            .Where(movie => movie.Hide == false && movie.Type == "Theater")
                            //.OrderBy(movie => movie.C_ORDER)
                            .Take(12) // Limit by 12 movie for showing in index page
                            .ToList();

            return PartialView(viewList);
        }
        public ActionResult GetOnTVMovie()
        {
            var viewList = _db.Movies
                            .Where(movie => movie.Hide == false && movie.Type == "Theater")
                            .OrderBy(movie => movie.C_ORDER)
                            .Take(8) // Limit by 12 movie for showing in index page
                            .ToList();

            return PartialView(viewList);
        }
        public ActionResult GetCeleb()
        {
            var viewList = _db.Celebrities
                            .Where(celeb => celeb.Hide == false)
                            .OrderBy(celeb => celeb.C_ORDER)
                            .ToList();
            return PartialView(viewList);
        }
        public ActionResult GetNews()
        {
            var newsList = _db.News
                           .Where(news => news.Hide == false)
                           .OrderBy(news => news.InitDate)
                           .Take(5)
                           .ToList();
            return PartialView(newsList);
        }
        public ActionResult GetTrailer()
        {
            var listTrailer = _db.Movies
                                .Where(movie => movie.Hide == false && movie.Trailer != "")
                                .OrderBy(movie => movie.InitDate)
                                .ToList();
            return PartialView(listTrailer);
        }
        public ActionResult GetFooter()
        {
            return PartialView();
        }
    }
}