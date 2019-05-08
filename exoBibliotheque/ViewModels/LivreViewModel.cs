using exoBibliotheque.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exoBibliotheque.ViewModels
{
    public class LivreViewModel
    {
        /// <summary>
        /// Identifiant technique du livre
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Titre du livre 
        /// </summary>
        [Required]
        public string Titre { get; set; }
        /// <summary>
        /// Date de parution du livre
        /// </summary>
        [Required]
        [DatePast(ErrorMessage = Constants.ERROR_DATE_PARUTION_NON_PASSEE, ErrorMessageResourceName = "DateParution")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de parution")]
        public DateTime DateParution { get; set; }
        /// <summary>
        /// Identifiant de l'auteur du livre 
        /// </summary>
        [Required]
        [Display(Name ="Auteur")]
        public int AuteurId { get; set; }
    }
}