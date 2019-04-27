using exoBibliotheque.Models;
using exoBibliotheque.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using exoBibliotheque.ViewModels;

namespace exoBibliotheque.Controllers
{
    public class RechercherController : Controller
    {
        Dal dal;

        public RechercherController()
        {
            dal = new Dal(BddBouchon.Instance);
        }
        /// <summary>
        /// Affiche le formulaire permettant d'afficher le formulaire de saisie du mot recherché
        /// </summary>
        /// <returns>Vue Index</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Recherche les livres dont le titre ou le nom de l'auteur qui contient un texte
        /// </summary>
        /// <param name="texteCherche">Texte cherché (sans tenir compte de la casse)</param>
        /// <returns>Vue Livre</returns>
        public ActionResult Livre(string texteCherche)
        {
            //Contrôle des paramètres
            if (string.IsNullOrEmpty(texteCherche)) return View("Error");

            // Recherche des livres dont le titre contient le texte
            List<Livre> livres = dal.RechercherLivres(texteCherche);
            //Recherche les auteurs dont le nom contient le  texte
            List<Auteur> auteurs = dal.RechercherAuteurs(texteCherche);
            // Parcours les auteurs et ajouter leurs livres au résultat 
            foreach (Auteur auteur in auteurs)
            {
                List<Livre> livresParAuteur = dal.ObtenirLivresParAuteur(auteur.Id);
                livres= livres.Union(livresParAuteur).ToList();
            }

            // Si aucun livre trouvé, on retourne la vue 404
            //if (livres.Count == 0) return new HttpNotFoundResult();

            // Construction du ViewModel et affichage de la vue
            RechercheViewModel vm = new RechercheViewModel();
            vm.Texte = texteCherche;
            vm.Livres= livres;

            return View(vm);
        }

    }
}