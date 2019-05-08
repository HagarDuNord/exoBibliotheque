using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exoBibliotheque
{
    static public class Constants
    {
        public const string ERROR_TITRE_EXISTANT= "Ce titre de livre existe déjà";
        public const string ERROR_AUTEUR_INCONNU = "Cet auteur n'existe pas";
        public const string ERROR_DATE_PARUTION_NON_PASSEE = "La date de parution doit être inférieure à la date du jour";
    }
}