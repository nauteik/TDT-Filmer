using MovieWebsite.Models;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult getMenu()
        {
            var viewList = _db.Menus
                            .Where(menu => menu.Hide == false)
                            .OrderBy(menu => menu.C_Order)
                            .ToList();
            return PartialView(viewList);
        }
        public ActionResult getTheaterMovie()
        {
            var viewList = _db.Movies
                            .Where(movie => movie.Hide == false && movie.Type == "Theater")
                            .OrderBy(movie => movie.C_ORDER)
                            .Take(12) // Limit by 12 movie for showing in index page
                            .ToList();
                            
            return PartialView(viewList);
        }
        public ActionResult getSlider()
        {
            var viewList = _db.Movies
                            .Where(movie => movie.Hide == false && movie.Type == "Theater")
                            .OrderBy(movie => movie.C_ORDER)
                            .Take(12) // Limit by 12 movie for showing in index page
                            .ToList();

            return PartialView(viewList);
        }
        public ActionResult getOnTVMovie()
        {
            var viewList = _db.Movies
                            .Where(movie => movie.Hide == false && movie.Type == "Theater")
                            .OrderBy(movie => movie.C_ORDER)
                            .Take(8) // Limit by 12 movie for showing in index page
                            .ToList();

            return PartialView(viewList);
        }
        public ActionResult getCeleb() { 
            var viewList = _db.Celebrities
                            .Where(celeb => celeb.Hide == false)
                            .OrderBy(celeb => celeb.C_ORDER)
                            .ToList();
            return PartialView(viewList);
        }
    }
}