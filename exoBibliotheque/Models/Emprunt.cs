using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exoBibliotheque.Models
{
    public class Emprunt
    {
        //Identifiant technique de l'emprunt
        public int Id { get; set; }
        // Client qui a emprunté
        [Required]
        public virtual Client Client { get; set; }
        // Livre emprunté
        [Required]
        public virtual Livre Livre { get; set; }
        // Date d'emprunt du livre
        [Required]
        public DateTime DateEmprunt { get; set; }
        // Date de retour du livre. Si vide, l'emprunt est toujours en cours
        public DateTime? DateRetour { get; set; }
    }
}