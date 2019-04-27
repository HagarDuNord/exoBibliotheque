using System;
using System.Web.Mvc;
using exoBibliotheque.Controllers;
using exoBibliotheque.Models.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exoBibliotheque.Tests.Controllers
{
    [TestClass]
    public class AjouterControllerTest
    {
        Dal dal;
        [TestInitialize]
        public void Initialize()
        {
            dal = new Dal(BddBouchon.Instance);
        }
        /// <summary>
        /// Vérifie qu'un get affiche la vue attendue
        /// </summary>
        [TestMethod]
        public void Ajouter_Get()
        {
            Dal dal = new Dal(BddBouchon.Instance);
            AjouterController ajouterController = new AjouterController(dal);

            ActionResult resultat = ajouterController.Livre();
            //TODO : A finir
            Assert.Inconclusive();
        }

        /// <summary>
        /// Vérifie qu'un post avec un livre d'un auteur inconnu affiche la vue erreur
        /// </summary>
        [TestMethod]
        public void Ajouter_Post_Livre_AuteurInconnu()
        {
            //TODO : A implémenter
            Assert.Inconclusive();
        }
    }
}
