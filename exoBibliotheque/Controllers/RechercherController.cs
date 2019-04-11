using exoBibliotheque.Models;
using exoBibliotheque.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exoBibliotheque.Controllers
{
    public class RechercherController : Controller
    {
        Dal dal;

        public RechercherController()
        {
            dal = new Dal(BddBouchon.Instance);
        }
        // GET: Rechercher
        public ActionResult Livre(string texteCherche)
        {
            List<Livre> liste = dal.RechercherLivres(texteCherche);
            ViewData["TexteCherche"] = texteCherche;
            ViewData["Livres"] = liste;
            return View();
        }

        public ActionResult Auteur(string texteCherche)
        {
            List<Auteur> liste = dal.RechercherAuteurs(texteCherche);
            ViewData["TexteCherche"] = texteCherche;
            ViewData["Auteurs"] = liste;
            return View();
        }
    }
}