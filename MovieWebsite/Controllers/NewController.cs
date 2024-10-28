using MovieWebsite.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MovieWebsite.Controllers
{
    public class NewController : Controller
    {
        MovieWebsiteEntities _db = new MovieWebsiteEntities();
        // GET: New
        public ActionResult NewList(string keyword = null, int page = 1, int pageSize = 12)
        {
            var query = _db.News.Where(m => m.Hide == false).AsQueryable();
            if (string.IsNullOrWhiteSpace(keyword) == false)
            {
                query = query.Where(m => m.Title.Contains(keyword));
            }
            var newList = query.ToList();
            int pageCount = (int)Math.Ceiling(newList.Count / (double)pageSize);
            ViewBag.TotalNews = newList.Count;
            newList = newList
            .OrderBy(m => m.Hide)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            ViewBag.Keyword = keyword;
            ViewBag.Page = page;
            ViewBag.PageCount = pageCount;

            return View(newList);
        }
        public ActionResult NewDetail(string meta)
        {
            var new_instance = _db.News.FirstOrDefault(n => n.Meta == meta);
            if (new_instance == null)
                return View("NotFound");
            return View(new_instance);

        }
        public ActionResult NewPagination(int currPage, int totalPages)
        {
            ViewBag.currPage = currPage;
            ViewBag.totalPages = totalPages;
            return PartialView();
        }
        public ActionResult PopularNews()
        {
            var popNews = _db.News
                           .OrderBy(n => n.C_ORDER)
                           .Take(5)
                           .ToList();
            return PartialView(popNews);
        }
    }

}