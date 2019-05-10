using System;
using System.Web.Mvc;
using exoBibliotheque.Controllers;
using exoBibliotheque.Models.DataAccess;
using exoBibliotheque.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exoBibliotheque.Tests.Controllers
{
    [TestClass]
    public class RechercherControllerTest
    {
        private Dal dal;
        private RechercherController rechercherController;

        [TestInitialize]
        public void Initialize()
        {
            dal = new Dal(BddBouchon.Instance);
            rechercherController = new RechercherController(dal);
        }

        /// <summary>
        /// Test une recherche qui ramène des résultats.
        /// </summary>
        [TestMethod]
        public void RechercherController_Resultat_1livre()
        {
            ActionResult actionResult=rechercherController.Livre("shi");
            ViewResult viewResult = (ViewResult)actionResult;
            RechercheViewModel rechercheViewModel = (RechercheViewModel)viewResult.Model;

            Assert.AreEqual(viewResult.MasterName, "");
            Assert.IsNotNull(viewResult.Model);
            Assert.AreEqual(rechercheViewModel.Livres.Count,1);

        }
        /// <summary>
        /// Test une recherche infructueuse
        /// </summary>
        [TestMethod]
        public void RechercherController_Resultat_0Livre()
        {
            ActionResult actionResult = rechercherController.Livre("livre inconnu");
            ViewResult viewResult = (ViewResult)actionResult;
            RechercheViewModel rechercheViewModel = (RechercheViewModel)viewResult.Model;

            Assert.AreEqual(viewResult.MasterName, "");
            Assert.IsNotNull(viewResult.Model);
            Assert.AreEqual(rechercheViewModel.Livres.Count, 0);

        }
    }
}
