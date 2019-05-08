using System;
using exoBibliotheque.Validators;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace exoBibliotheque.Tests.Validators
{
    /// <summary>
    /// Test le validateur d'attribut DatePast
    /// </summary>
    [TestClass]
    public class DatePastUnitTest
    {
        private class ModeleDate
        {
            [DatePast]
            public DateTime Valeur { get; set; }
        }
        
        /// <summary>
        /// Vérifie qu'une date dans le passée (hier) est valide
        /// </summary>
        [TestMethod]
        public void DatePast_Date_Passee()
        {
            ModeleDate modeleDateHier = new ModeleDate() { Valeur = DateTime.Today.AddDays(-1) };
            ValidationContext context = new ValidationContext(modeleDateHier);
            DatePast datePast = new DatePast();
            ValidationResult validationResult=datePast.GetValidationResult(modeleDateHier.Valeur, context);
            Assert.AreEqual(validationResult, ValidationResult.Success);
        }
        /// <summary>
        /// Vérifie que la date du jour est invalide
        /// </summary>
        [TestMethod]
        public void DatePast_Date_Aujourdhui()
        {
            ModeleDate modeleDateAujourdhui = new ModeleDate() { Valeur = DateTime.Today };
            ValidationContext context = new ValidationContext(modeleDateAujourdhui);
            DatePast datePast = new DatePast();
            ValidationResult validationResult = datePast.GetValidationResult(modeleDateAujourdhui.Valeur, context);
            Assert.AreNotEqual(validationResult, ValidationResult.Success);
        }

        /// <summary>
        /// Vérifie qu'une date dans le futur (demain) est invalide
        /// </summary>
        [TestMethod]
        public void DatePast_Date_Futur()
        {
            ModeleDate modeleDateDemain = new ModeleDate() { Valeur = DateTime.Today.AddDays(1) };
            ValidationContext context = new ValidationContext(modeleDateDemain);
            DatePast datePast = new DatePast();
            ValidationResult validationResult = datePast.GetValidationResult(modeleDateDemain.Valeur, context);
            Assert.AreNotEqual(validationResult, ValidationResult.Success);
        }
    }

    
}
