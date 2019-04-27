using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exoBibliotheque.Models;

namespace exoBibliotheque.ViewModels
{
    /// <summary>
    /// Résultat de la recherche d'un texte dans le titre des livres et nom des auteurs
    /// </summary>
    public class RechercheViewModel
    {
        /// <summary>
        /// Texte qui a été cherché
        /// </summary>
        public string Texte { get; set; }
        /// <summary>
        /// Liste des livres trouvés
        /// </summary>
        public List<Livre> Livres { get; set; }
    }
}