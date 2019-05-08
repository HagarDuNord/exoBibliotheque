using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exoBibliotheque.Validators
{
    /// <summary>
    /// Attribut de validation personnalisée pour tester qu'une date est dans le passé.
    /// </summary>
    public class DatePast : ValidationAttribute, IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule regle = new ModelClientValidationRule();
            regle.ValidationType = "datepast";
            regle.ErrorMessage = GetErrorMessage();
            return new List<ModelClientValidationRule> { regle };
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            string[] memberName = new string[] { validationContext.MemberName };
            if (value==null || (DateTime)value >= DateTime.Today)
            {
                
                return new ValidationResult(GetErrorMessage(),memberName);
            }
                
            return ValidationResult.Success;
        }
        /// <summary>
        /// Retourne un message d'erreur par défaut si le message d'erreur n'a pas été précisé lors de la définition de l'attribut
        /// </summary>
        /// <returns></returns>
        private string GetErrorMessage()
        {
            
            if(string.IsNullOrEmpty(ErrorMessage))
                return $"La date doit être strictement inférieure à la date du jour.";
            return ErrorMessage;
        }
    }

}