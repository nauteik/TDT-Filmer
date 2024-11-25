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
            ViewBag.NewId = new_instance.Id;
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
        [HttpPost]
        public ActionResult AddComment(int newId, int userId, string content)
        {
            try 
            {
                // Validate userId matches Session
                if (Session["UserId"] == null || userId != (int)Session["UserId"])
                {
                    return Json(new { success = false, message = "Unauthorized" });
                }

                // Thêm comment vào database
                var comment = new NewComment
                {
                    NewId = newId,
                    UserId = userId,
                    Content = content,
                    InitDate = DateTime.Now,
                    Hide = false
                };
                
                _db.NewComments.Add(comment);
                _db.SaveChanges();

                // Lấy thông tin user để trả về
                var user = _db.Users.Find(userId);
                
                // Trả về dữ liệu comment mới để cập nhật UI
                return Json(new { 
                    success = true, 
                    comment = new {
                        id = comment.Id,
                        content = comment.Content,
                        initDate = comment.InitDate.Value.ToString("dd MMM yyyy"),
                        userFullName = $"{user.FirstName} {user.LastName}",
                        userAvatar = user.Avatar
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}