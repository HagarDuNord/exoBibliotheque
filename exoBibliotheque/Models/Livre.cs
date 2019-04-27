using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exoBibliotheque.Models
{
    public class Livre
    {
        /// <summary>
        /// Identifiant technique du livre
        /// </summary>
        public int Id { get; set;}
        /// <summary>
        /// Titre du livre 
        /// </summary>
        [Required]
        public string Titre { get; set; }
        /// <summary>
        /// Date de parution du livre
        /// </summary>
        public DateTime DateParution { get; set; }
        /// <summary>
        /// Auteur du livre 
        /// </summary>
        public virtual Auteur Auteur { get; set; }
    }
}