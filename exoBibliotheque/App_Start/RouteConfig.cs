using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace exoBibliotheque
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Afficher",
                url: "Afficher/{action}/{id}",
                defaults: new { controller = "Afficher", action = "Livres", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Rechercher",
                url: "Rechercher/{action}/{texteCherche}",
                defaults: new { controller = "Rechercher", texteCherche = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
