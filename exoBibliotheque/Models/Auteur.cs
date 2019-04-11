using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exoBibliotheque.Models
{
    public class Auteur
    {

        // Identifiant technique de l'auteur
        public int Id { get; set; }
        // Nom de l'auteur
        [Required]
        public string Nom { get; set; }
    }
}