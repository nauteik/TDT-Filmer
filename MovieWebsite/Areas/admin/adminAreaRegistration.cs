using System.Web.Mvc;

namespace MovieWebsite.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "AdminLogin",
                url: "admin/login",
                defaults: new { controller = "AdminDefault", action = "Login" }
            );

            context.MapRoute(
                name: "AdminHome",
                url: "admin",
                defaults: new { controller = "AdminDefault", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "admin_default",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}