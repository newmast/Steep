﻿namespace Steep.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "StoriesRoute",
                url: "story/{id}",
                defaults: new { controller = "Story", action = "Details" });

            routes.MapRoute(
                name: "UsersRoute",
                url: "Users/{id}",
                defaults: new { controller = "Users", action = "Details" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
