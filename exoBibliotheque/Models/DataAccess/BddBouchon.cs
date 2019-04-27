using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exoBibliotheque.Models.DataAccess
{
    /*
     * Dans cette premiere version. On crée la base de manière statique
     */
    public sealed class BddBouchon : IBdd
    {
        private static BddBouchon instance = null;
        private static readonly object padlock = new object();

        public List<Auteur> Auteurs { get; set; }
        public List<Livre> Livres { get; set; }
        public List<Client> Clients { get; set; }
        public List<Emprunt> Emprunts { get; set; }
                
        private BddBouchon()
        {
            // Initialisation des auteurs
            Auteur auteur1 = new Auteur { Id = 1, Nom = "Stephen King" };
            Auteur auteur2 = new Auteur { Id = 2, Nom = "Victor Hugo" };
            Auteur auteur3 = new Auteur { Id = 3, Nom = "Lewis Carroll" };
            Auteur auteur4 = new Auteur { Id = 4, Nom = "Isaac Asimov" };
            Auteurs = new List<Auteur> { auteur1, auteur2, auteur3, auteur4 };
            // Initialisation des livres
            Livre livre1 = new Livre { Id = 1, Titre = "Shinning", DateParution = new DateTime(1977, 1, 28), Auteur = auteur1 };
            Livre livre2 = new Livre { Id = 2, Titre = "Docteur Sleep", DateParution = new DateTime(2013, 9, 24), Auteur = auteur1 };
            Livre livre3 = new Livre { Id = 3, Titre = "Misery", DateParution = new DateTime(1987, 6, 8), Auteur = auteur1 };
            Livre livre4 = new Livre { Id = 4, Titre = "Notre-Dame de Paris", DateParution = new DateTime(1831, 3, 1), Auteur = auteur2 };
            Livre livre5 = new Livre { Id = 5, Titre = "Les Aventures d’Alice au pays des merveilles", DateParution = new DateTime(1865, 7, 4), Auteur = auteur3 };
            Livres = new List<Livre> { livre1, livre2, livre3, livre4, livre5 };
            // Initialisation des clients
            Client client1 = new Client { Email = "kaiser.franck@bayern.de", Nom = "Franck Ribery" };
            Client client2 = new Client { Email = "steevy.boulay@gmail.com", Nom = "Steevy Boulay" };
            Clients = new List<Client> { client1, client2 };
            // Initialisation des emprunts
            Emprunt emprunt1 = new Emprunt { Id = 1, Client = client1, Livre = livre1, DateEmprunt = DateTime.Now };
            Emprunt emprunt2 = new Emprunt { Id = 2, Client = client1, Livre = livre2, DateEmprunt = DateTime.Now };
            Emprunt emprunt3 = new Emprunt { Id = 3, Client = client2, Livre = livre3, DateEmprunt = DateTime.Now };
            Emprunts = new List<Emprunt> { emprunt1, emprunt2, emprunt3 };


        }

        public static BddBouchon Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new BddBouchon();
                    }
                    return instance;
                }
            }
        }
    }
}