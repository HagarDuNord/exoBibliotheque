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
            else
            {
                if (dal.LivreExiste(livre.Titre))
                {
                    modeleOk = false;
                    ModelState.AddModelError("Titre", "Ce titre de livre existe déjà");
                }
            }
            if (! modeleOk)
            {
                AlimenterListeAuteur(livre.AuteurId);
                return View(livre);
            }
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