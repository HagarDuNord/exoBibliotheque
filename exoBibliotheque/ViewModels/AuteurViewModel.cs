using exoBibliotheque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exoBibliotheque.ViewModels
{
    public class AuteurViewModel
    {
        public Auteur Auteur { get; set; }
        public List<Livre> Livres { get; set; }
    }
}