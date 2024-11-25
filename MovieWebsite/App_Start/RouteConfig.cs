using System.Web.Mvc;
using System.Web.Routing;

namespace MovieWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "celebList",
                url: "celebrity",
                defaults: new { controller = "Celeb", action = "CelebList", page = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "movie",
                url: "movie",
                defaults: new { controller = "Movie", action = "Index", page = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MovieInfo",
                url: "movie/{meta}",
                defaults: new { controller = "Movie", action = "Detail", meta = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Homepage",
               url: "trang-chu",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "CelebDetail",
               url: "celebrity/{meta}",
               defaults: new { controller = "Celeb", action = "Detail", meta = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "NewComment",
               url: "New/AddComment",
               defaults: new { controller = "New", action = "AddComment" }
           );
            routes.MapRoute(
               name: "LoadMoreComments",
               url: "New/LoadMoreComments",
               defaults: new { controller = "New", action = "LoadMoreComments" }
           );
            routes.MapRoute(
               name: "new",
               url: "new",
               defaults: new { controller = "New", action = "NewList", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "newDetail",
               url: "new/{meta}",
               defaults: new { controller = "New", action = "NewDetail", meta = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "UserProfile",
               url: "user/profile",
               defaults: new { controller = "Account", action = "Profile" }
           );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}
