using exoBibliotheque.Models;
using exoBibliotheque.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exoBibliotheque.Controllers
{
    public class AfficherController : Controller
    {
        Dal dal;

        public AfficherController()
        {
            dal = new Dal(BddBouchon.Instance);
        }

        // GET: Afficher
        public ActionResult Livres()
        {
           
            ViewData["Livres"] = dal.ObtenirTousLesLivres();
            return View();
        }
        public ActionResult Livre(string id)
        {
            int idLivre;
            if (id is null || (!int.TryParse(id, out idLivre))) return View("Error");
            Livre livre = dal.ObtenirLivre(idLivre);
            if (livre is null) return View("Error");
            ViewData["Livre"] = livre;
            Emprunt emprunt = dal.ObtenirEmpruntActifParLivre(idLivre);
            ViewData["Emprunt"] = emprunt;
            return View();
        }
        public ActionResult Auteurs()
        {
            ViewData["Auteurs"] = dal.ObtenirTousLesAuteurs();
            return View();
        }
        public ActionResult Auteur(string id)
        {
            int idAuteur;
            if (id is null || (!int.TryParse(id, out idAuteur))) return View("Error");
            Auteur auteur = dal.ObtenirAuteur(idAuteur);
            if (auteur is null) return View("Error");
            List<Livre> livres = dal.ObtenirLivresParAuteur(idAuteur);
            ViewData["Auteur"] = auteur;
            ViewData["Livres"] = livres;
            return View();
        }
    }
}