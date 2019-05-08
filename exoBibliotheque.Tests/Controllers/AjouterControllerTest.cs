using System;
using System.Web.Mvc;
using exoBibliotheque.Controllers;
using exoBibliotheque.Models.DataAccess;
using exoBibliotheque.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exoBibliotheque.Tests.Controllers
{
    [TestClass]
    public class AjouterControllerTest
    {
        private Dal dal;
        private AjouterController ajouterController;
        [TestInitialize]
        public void Initialize()
        {
            dal = new Dal(BddBouchon.Instance);
            ajouterController = new AjouterController(dal);
        }

        /// <summary>
        /// Vérifie qu'un get affiche la vue attendue
        /// </summary>
        [TestMethod]
        public void Ajouter_Get()
        {
            
            
            ActionResult resultat = ajouterController.Livre();
            ViewResult view = (ViewResult)resultat;
            Assert.AreEqual(view.ViewName,"");
        }

        /// <summary>
        /// Vérifie qu'un post avec un livre d'un auteur inconnu réaffiche la vue avec un message d'erreur
        /// </summary>
        [TestMethod]
        public void Ajouter_Post_Livre_AuteurInconnu()
        {
            LivreViewModel livre = new LivreViewModel { Titre = "Livre_AuteurInconnu", DateParution = DateTime.Today.AddDays(-1), AuteurId = 99 };
            ActionResult resultat = ajouterController.Livre(livre);

            ViewResult view = (ViewResult)resultat;
            Assert.AreEqual(view.Model, livre);
            ModelStateDictionary modelStateDictionary = ajouterController.ModelState;
            ModelState modelState;
            modelStateDictionary.TryGetValue("AuteurId", out modelState);
            Assert.AreEqual(modelState.Errors.Count, 1);
            string message = modelState.Errors[0].ErrorMessage;
            Assert.AreEqual(message, Constants.ERROR_AUTEUR_INCONNU);
        }
        /// <summary>
        /// Vérifie qu'un post avec un livre déjà présente réaffiche la vue avec un message d'erreur
        /// </summary>
        [TestMethod]
        public void Ajouter_Post_Livre_Titre_DejaPresent()
        {
            LivreViewModel livre = new LivreViewModel { Titre = "Shinning", DateParution = DateTime.Today.AddDays(-1), AuteurId = 1 };
            ActionResult resultat = ajouterController.Livre(livre);

            ViewResult view = (ViewResult)resultat;
            Assert.AreEqual(view.Model, livre);
            ModelStateDictionary modelStateDictionary = ajouterController.ModelState;
            ModelState modelState;
            modelStateDictionary.TryGetValue("Titre", out modelState);
            Assert.AreEqual(modelState.Errors.Count, 1);
            string message = modelState.Errors[0].ErrorMessage;
            Assert.AreEqual(message, Constants.ERROR_TITRE_EXISTANT);
        }
        /// <summary>
        /// Vérifie qu'un post avec un livre sans titre réaffiche la vue avec un message d'erreur
        /// </summary>
        [TestMethod]
        public void Ajouter_Post_Livre_Titre_NonRenseigne()
        {
            //LivreViewModel livre = new LivreViewModel { Titre = "Livre parue dans le futur", DateParution = DateTime.Today.AddDays(1), AuteurId = 1 };
            LivreViewModel livre = new LivreViewModel { Titre = "", DateParution=DateTime.Today, AuteurId = 1 };
            ajouterController.ValideLeModele(livre);
            ActionResult resultat = ajouterController.Livre(livre);

            ViewResult view = (ViewResult)resultat;
            Assert.AreEqual(view.Model, livre);
            ModelStateDictionary modelStateDictionary = ajouterController.ModelState;
            ModelState modelState;
            modelStateDictionary.TryGetValue("Titre", out modelState);
            Assert.AreEqual(modelState.Errors.Count, 1);
            string message = modelState.Errors[0].ErrorMessage;
        }

        

        /// <summary>
        /// Vérifie qu'un post avec un livre paru demain réaffiche la vue avec un message d'erreur
        /// </summary>
        [TestMethod]
        public void Ajouter_Post_Livre_DateParution_Future()
        {
            LivreViewModel livre = new LivreViewModel { Titre = "Livre parue dans le futur", DateParution = DateTime.Today.AddDays(1), AuteurId = 1 };
            ajouterController.ValideLeModele(livre);
            ActionResult resultat = ajouterController.Livre(livre);

            ViewResult view = (ViewResult)resultat;
            Assert.AreEqual(view.Model, livre);
            ModelStateDictionary modelStateDictionary = ajouterController.ModelState;
            ModelState modelState;
            modelStateDictionary.TryGetValue("DateParution", out modelState);
            Assert.AreEqual(modelState.Errors.Count, 1);
            string message = modelState.Errors[0].ErrorMessage;
            Assert.AreEqual(message, Constants.ERROR_DATE_PARUTION_NON_PASSEE);
        }
        /// <summary>
        /// Vérifie qu'un post avec un livre paru aujourdhui réaffiche la vue avec un message d'erreur
        /// </summary>
        [TestMethod]
        public void Ajouter_Post_Livre_DateParution_Aujourdhui()
        {
            LivreViewModel livre = new LivreViewModel { Titre = "Livre parue aujourd'hui", DateParution = DateTime.Today, AuteurId = 1 };
            ajouterController.ValideLeModele(livre);
            ActionResult resultat = ajouterController.Livre(livre);

            ViewResult view = (ViewResult)resultat;
            Assert.AreEqual(view.Model, livre);
            ModelStateDictionary modelStateDictionary = ajouterController.ModelState;
            ModelState modelState;
            modelStateDictionary.TryGetValue("DateParution", out modelState);
            Assert.AreEqual(modelState.Errors.Count, 1);
            string message = modelState.Errors[0].ErrorMessage;
            Assert.AreEqual(message, Constants.ERROR_DATE_PARUTION_NON_PASSEE);
        }
        /// <summary>
        /// Vérifie qu'un post avec un livre correctement rensiegné créé le livre et affiche la vue Index
        /// </summary>
        [TestMethod]
        public void Ajouter_Post_Livre_OK()
        {
            
            LivreViewModel livre = new LivreViewModel { Titre = "Nouveau livre", DateParution = DateTime.Today.AddDays(-1), AuteurId = 1 };
            

            // Vérifie que le livre n'existe pas avant l'appel au controleur
            Assert.IsFalse(dal.LivreExiste(livre.Titre));
            ajouterController.ValideLeModele(livre);
            ActionResult resultat = ajouterController.Livre(livre);
            // Vérifie que le livre existe aprés l'appel au controleur
            Assert.IsTrue(dal.LivreExiste(livre.Titre));

            // Vérifie qu'on est bien redirigé vers l'action Index du controller Afficher
            RedirectToRouteResult redirectToRoute = (RedirectToRouteResult)resultat;
            Assert.AreEqual(redirectToRoute.RouteValues["action"], "Index");
            Assert.AreEqual(redirectToRoute.RouteValues["controller"], "Afficher");
        }
    }
}
