using MovieWebsite.Areas.admin.Models;
using MovieWebsite.Models;
using System.Linq;
using System.Web.Mvc;

namespace MovieWebsite.Areas.admin.Controllers
{
    public class AdminDefaultController : Controller
    {
        private MovieWebsiteEntities db = new MovieWebsiteEntities();

        // GET: admin/AdminDefault
        public ActionResult Index()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Staff")
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            // Nếu đã đăng nhập và là Staff thì chuyển về Index
            if (Session["UserId"] != null && Session["Role"].ToString() == "Staff")
            {
                return RedirectToAction("Index");
            }
            return View(new AdminLogin());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminLogin model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u =>
                    u.Username == model.Username &&
                    u.Password == model.Password &&
                    u.Role == "Staff");

                if (user != null)
                {
                    Session["UserId"] = user.Id;
                    Session["Username"] = user.Username;
                    Session["Role"] = user.Role;
                    
                    // Kiểm tra và xử lý avatar
                    var avatar = string.IsNullOrEmpty(user.Avatar) ? "defaultavatar.png" : user.Avatar;
                    Session["Avatar"] = avatar;
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}