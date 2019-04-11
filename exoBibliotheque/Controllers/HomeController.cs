using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exoBibliotheque.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Exercice création bibliotheque";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "mickael.dupart@gmail.com";

            return View();
        }
    }
}