using System.Web.Mvc;
using System.Web.Routing;

namespace MovieWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "celebList",
                url: "celebrity",
                defaults: new { controller = "Celeb", action = "CelebList", page = UrlParameter.Optional, id = UrlParameter.Optional },
                namespaces: new[] { "MovieWebsite.Controllers" }
            );
            routes.MapRoute(
                name: "movie",
                url: "movie",
                defaults: new { controller = "Movie", action = "Index", page = UrlParameter.Optional, id = UrlParameter.Optional },
                namespaces: new[] { "MovieWebsite.Controllers" }
            );
            routes.MapRoute(
                name: "MovieInfo",
                url: "movie/{meta}",
                defaults: new { controller = "Movie", action = "Detail", meta = UrlParameter.Optional },
                namespaces: new[] { "MovieWebsite.Controllers" }
            );

            routes.MapRoute(
               name: "Homepage",
               url: "trang-chu",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MovieWebsite.Controllers" }
           );
            routes.MapRoute(
               name: "CelebDetail",
               url: "celebrity/{meta}",
               defaults: new { controller = "Celeb", action = "Detail", meta = UrlParameter.Optional },
               namespaces: new[] { "MovieWebsite.Controllers" }
           );
            routes.MapRoute(
               name: "NewComment",
               url: "New/AddComment",
               defaults: new { controller = "New", action = "AddComment" },
               namespaces: new[] { "MovieWebsite.Controllers" }
           );
            routes.MapRoute(
               name: "LoadMoreComments",
               url: "New/LoadMoreComments",
               defaults: new { controller = "New", action = "LoadMoreComments" },
               namespaces: new[] { "MovieWebsite.Controllers" }
           );
            routes.MapRoute(
               name: "new",
               url: "new",
               defaults: new { controller = "New", action = "NewList", id = UrlParameter.Optional },
               namespaces: new[] { "MovieWebsite.Controllers" }
           );
            routes.MapRoute(
               name: "newDetail",
               url: "new/{meta}",
               defaults: new { controller = "New", action = "NewDetail", meta = UrlParameter.Optional },
               namespaces: new[] { "MovieWebsite.Controllers" }
           );
            routes.MapRoute(
               name: "UserProfile",
               url: "user/profile",
               defaults: new { controller = "Account", action = "Profile" },
               namespaces: new[] { "MovieWebsite.Controllers" }
           );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               namespaces: new[] { "MovieWebsite.Controllers" },
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}
