using exoBibliotheque.Models;
using exoBibliotheque.Models.DataAccess;
using exoBibliotheque.ViewModels;
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

        
        /// <summary>
        /// Affiche la liste des livres
        /// </summary>
        /// <returns>Vue Index</returns>
        public ActionResult Index()
        {
           
            List<Livre> vm = dal.ObtenirTousLesLivres();
            return View(vm);
        }
        /// <summary>
        /// Affiche le détatil d'un livre et le nom de l'emprunteur
        /// </summary>
        /// <param name="id">Id technique du livre à afficher</param>
        /// <returns>Vue Livre</returns>
        public ActionResult Livre(string id)
        {
            // Contrôle des paramètres
            int idLivre;
            if (string.IsNullOrEmpty(id) || (!int.TryParse(id, out idLivre))) return View("Error");
            // Recherche du livre
            Livre livre = dal.ObtenirLivre(idLivre);
            if (livre ==null) return new HttpNotFoundResult();
            
            // Récupérer l'emprunt courant du livre
            Emprunt emprunt = dal.ObtenirEmpruntActifParLivre(idLivre);
            // Construction du viewModel et affichage de la vue
            LivreDetailViewModel livreViewModel = new LivreDetailViewModel();
            livreViewModel.Livre = livre;
            if (emprunt!=null) { 
                livreViewModel.NomEmpruteur = emprunt.Client.Nom;
            }
            return View(livreViewModel);

        }
        /// <summary>
        /// Affiche la liste des auteurs
        /// </summary>
        /// <returns>Vue Auteurs</returns>
        public ActionResult Auteurs()
        {
            List<Auteur>  vm = dal.ObtenirTousLesAuteurs();
            return View(vm);
        }
        /// <summary>
        /// Affiche le détail d'un auteur et la liste des livres
        /// </summary>
        /// <param name="id">Id technique de l'auteur</param>
        /// <returns>Vue Auteur</returns>
        public ActionResult Auteur(string id)
        {
            // Controle des paramètres
            int idAuteur;
            if (string.IsNullOrEmpty(id) || (!int.TryParse(id, out idAuteur))) return View("Error");
            // Recherche de l'auteur
            Auteur auteur = dal.ObtenirAuteur(idAuteur);
            if (auteur is null) return new HttpNotFoundResult();
            //Récupère les livres de l'auteur
            List<Livre> livres = dal.ObtenirLivresParAuteur(idAuteur);
            // Contrusction du viewModel et affichage de la vue
            AuteurViewModel vm = new AuteurViewModel();
            vm.Auteur = auteur;
            vm.Livres = livres;
            return View(vm);
        }
    }
}