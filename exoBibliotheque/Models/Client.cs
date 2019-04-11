using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exoBibliotheque.Models
{
    public class Client
    {
        // Email du client. Sert d'identifiant
        [Key,MaxLength(250)]
        public string Email { get; set; }
        // Nom du client
        [Required]
        public string Nom { get; set; }
    }
}