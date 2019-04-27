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
        [Required,DatePast(ErrorMessage = "La date de parution doit être inférieure à la date du jour")]
        [Display(Name = "Date de parution (au format AAAA-MM-JJ)")]
        public DateTime DateParution { get; set; }
        /// <summary>
        /// Identifiant de l'auteur du livre 
        /// </summary>
        [Required]
        [Display(Name ="Auteur")]
        public int AuteurId { get; set; }
    }
}