using exoBibliotheque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exoBibliotheque.ViewModels
{
    public class LivreDetailViewModel
    {
        public Livre Livre { get; set; }
        public string NomEmpruteur { get; set; }

    }
}