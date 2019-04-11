using System.Collections.Generic;

namespace exoBibliotheque.Models.DataAccess
{
    public interface IBdd
    {
        List<Auteur> Auteurs { get; set; }
        List<Client> Clients { get; set; }
        List<Emprunt> Emprunts { get; set; }
        List<Livre> Livres { get; set; }
    }
}