using exoBibliotheque.Models;
using exoBibliotheque.ViewModels;
using exoBibliotheque.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exoBibliotheque.Controllers
{
    public class AjouterController : Controller
    {
        Dal dal;
        /// <summary>
        /// Par défaut, on créé un controlleur avec la base bouchon
        /// </summary>
        public AjouterController() : this(new Dal(BddBouchon.Instance))
        {
        }
        /// <summary>
        /// Permet de spécifier un dal associé au controlleur
        /// </summary>
        /// <param name="dalIoc">dal associé au controlleur</param>
        public AjouterController(Dal dalIoc)
        {
            dal = dalIoc;
        }
        // GET: Ajouter
        public ActionResult Livre()
        {
            AlimenterListeAuteur(null);
            return View();
        }

        [HttpPost]
        public ActionResult Livre(LivreViewModel livre)
        {
            bool modeleOk = true;
            if (!ModelState.IsValid)
            {
                modeleOk = false;
                
            }
            // Vérifie que l'auteur existe
            if (dal.ObtenirAuteur(livre.AuteurId)==null)
            {
                modeleOk = false;
                ModelState.AddModelError("AuteurId", Constants.ERROR_AUTEUR_INCONNU);
            }

            // Vérifie qu'il n'existe pas déjà un livre avec le même titre
            if (dal.LivreExiste(livre.Titre))
            {
                modeleOk = false;
                ModelState.AddModelError("Titre", Constants.ERROR_TITRE_EXISTANT);
            }
            // Si une erreur a été détectée, on renvoie vers la page avec le modèle
            if (! modeleOk)
            {
                AlimenterListeAuteur(livre.AuteurId);
                return View(livre);
            }
            // Pas d'erreur, on créé le livre et on réaffiche la liste des livres
            Livre livreCreer = dal.CreerLivre(livre.Titre, livre.DateParution, livre.AuteurId);
            return RedirectToAction("Index","Afficher");
        }

        private void AlimenterListeAuteur(int? idSelectionne)
        {
            List<Auteur> auteurs = dal.ObtenirTousLesAuteurs();
            if (idSelectionne!=null) { 
                ViewBag.Auteurs = new SelectList(auteurs, "Id", "Nom",idSelectionne);
            }
            else
            {
                ViewBag.Auteurs = new SelectList(auteurs, "Id", "Nom");
            }
        }
    }
}