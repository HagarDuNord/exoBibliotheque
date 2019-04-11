using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exoBibliotheque.Models
{
    public class Livre
    {
        // Identifiant technique du livre
        public int Id { get; set;}
        // Titre du livre
        [Required]
        public string Titre { get; set; }
        // Date de parution du livre
        public DateTime DateParution { get; set; }
        // Auteur du livre
        public virtual Auteur Auteur { get; set; }
    }
}