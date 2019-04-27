using System;
using exoBibliotheque.Validators;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exoBibliotheque.Tests.Validators
{
    /// <summary>
    /// Test le validateur d'attribut DatePast
    /// </summary>
    [TestClass]
    public class DatePastUnitTest
    {

        
        /// <summary>
        /// Vérifie qu'une date dans le passée (hier) est valide
        /// </summary>
        [TestMethod]
        public void DatePast_Date_RefactoPassee()
        {
            DateTime testHier = DateTime.Today.AddDays(-1);
            DatePast datePast = new DatePast();
            bool resultat = datePast.IsValid(testHier);
            Assert.IsTrue(resultat);
        }
        /// <summary>
        /// Vérifie que la date du jour est invalide
        /// </summary>
        [TestMethod]
        public void DatePast_Date_Aujourdhui()
        {
            DateTime testAujourdhui = DateTime.Today;
            DatePast datePast = new DatePast();
            bool resultat = datePast.IsValid(testAujourdhui);
            Assert.IsFalse(resultat);
        }

        /// <summary>
        /// Vérifie qu'une date dans le futur (demain) est invalide
        /// </summary>
        [TestMethod]
        public void DatePast_Date_Futur()
        {
            DateTime testDemain = DateTime.Today.AddDays(1);
            DatePast datePast = new DatePast();
            bool resultat = datePast.IsValid(testDemain);
            Assert.IsFalse(resultat);
        }
    }

    
}
