using MovieWebsite.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MovieWebsite.Controllers
{
    public class AccountController : Controller
    {
        private MovieWebsiteEntities db = new MovieWebsiteEntities();

        [HttpPost]
        public JsonResult Login(string username, string password, bool rememberMe)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe);
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                Session["Avatar"] = user.Avatar ?? "defaultavatar.png";
                return Json(new { success = true, username = user.Username });
            }

            return Json(new { success = false, message = "Invalid username or password" });
        }

        [HttpPost]
        public JsonResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return Json(new { success = true });
        }

        [Authorize]
        public ActionResult Profile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int userId = (int)Session["UserId"];
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public JsonResult UpdateProfile(User model)
        {
            if (Session["UserId"] == null)
            {
                return Json(new { success = false, message = "Not logged in" });
            }

            if (ModelState.IsValid)
            {
                int userId = (int)Session["UserId"];
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Country = model.Country;

                    db.SaveChanges();
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false, message = "Failed to update profile" });
        }

        [HttpPost]
        public JsonResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                // Debug: Log input parameters
                System.Diagnostics.Debug.WriteLine($"ChangePassword called with oldPassword: {oldPassword?.Length}, newPassword: {newPassword?.Length}");

                if (Session["UserId"] == null)
                {
                    return Json(new { success = false, message = "Not logged in" });
                }

                // Validate passwords match
                if (newPassword != confirmPassword)
                {
                    return Json(new { success = false, message = "New passwords do not match" });
                }

                int userId = (int)Session["UserId"];
                var user = db.Users.Find(userId);

                // Debug: Log user lookup
                System.Diagnostics.Debug.WriteLine($"Found user: {user != null}, UserId: {userId}");

                if (user == null)
                {
                    return Json(new { success = false, message = "User not found" });
                }

                // Debug: Log password comparison
                System.Diagnostics.Debug.WriteLine($"Current password match: {user.Password == oldPassword}");

                if (user.Password != oldPassword)
                {
                    return Json(new { success = false, message = "Current password is incorrect" });
                }

                user.Password = newPassword;
                db.SaveChanges();

                // Debug: Log success
                System.Diagnostics.Debug.WriteLine("Password changed successfully");

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Debug: Log exception details
                System.Diagnostics.Debug.WriteLine($"Exception in ChangePassword: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}