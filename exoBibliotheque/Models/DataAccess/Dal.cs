using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exoBibliotheque.Models.DataAccess
{
    public class Dal
    {
        private IBdd bdd;
        static int MAX_LIVRE_EMPRUNTE = 2;

        public Dal(IBdd bdd)
        {
            this.bdd = bdd;
        }

        public bool ClientExiste(string email)
        {
            Client clientTrouve=bdd.Clients.FirstOrDefault(client => client.Email==email);
            return (clientTrouve!= null);
        }

        public Client CreerClient(string email, string nom)
        {
            // Vérifie s'il existe déjà un client avec cette adresse mail
            bool clientExiste=ClientExiste(email);
            // Si pas de client trouvé, on le créé et on le retourne
            if (! clientExiste)
            {
                Client client = new Client { Email = email, Nom = nom };
                bdd.Clients.Add(client);
                return client;
            }
            // Le client n'existe pas, on retourne Null
            return null;
        }
        public bool LivreExiste(string titre)
        {
            Livre livreTrouve = bdd.Livres.FirstOrDefault(livre => livre.Titre == titre);
            return (livreTrouve != null);
        }
        public Livre CreerLivre(string titre, DateTime dateParution, int idAuteur)
        {
            Auteur auteur = ObtenirAuteur(idAuteur);
            if (auteur == null) return null;
            //TODO : A Supprimer lorsqu'on aura une persistance en base
            int idLivre;
            if (bdd.Livres.Count > 0)
            {
                idLivre = bdd.Livres.Max(item => item.Id) + 1;
            }
            else
            {
                idLivre = 1;
            }

            Livre livre = new Livre();
            livre.Id = idLivre;
            livre.Titre = titre;
            livre.DateParution = dateParution;
            livre.Auteur = auteur;
            bdd.Livres.Add(livre);
            return livre;
        }
        public Emprunt CreerEmprunt(int idLivre, string email, DateTime dateEmprunt)
        {
            Livre livre = ObtenirLivre(idLivre);
            Client client = ObtenirClient(email);
            // Si le livre ou le client n'existe pas, pas d'emprunt créé
            if (livre is null || client is null) return null;
            // Si le livre est déjà en cours d'emprunt, pas d'emprunt créé
            if (EmpruntActifParLivreExiste(idLivre)) return null;
            // Si le client a déjà emprunté le nombre max de livre, pas d'emprunt créé
            if (ObtenirEmpruntsActifParClient(email).Count>=MAX_LIVRE_EMPRUNTE) return null;
            // Recherche le prochain
            //TODO : A Supprimer lorsqu'on aura une persistance en base
            int idEmprunt;
            if (bdd.Emprunts.Count > 0)
            {
                idEmprunt = bdd.Emprunts.Max(empruntMax => empruntMax.Id) + 1;
            }
            else
            {
                idEmprunt = 0;
            }
            Emprunt emprunt = new Emprunt { Id=idEmprunt, Livre=livre, Client=client, DateEmprunt= dateEmprunt };
            return emprunt;

        }

        public bool EmpruntActifParLivreExiste(int idLivre)
        {
            //List<Emprunt> liste = bdd.Emprunts.FindAll(emprunt => emprunt.Livre.Id == idLivre && emprunt.DateRetour is null);
            Emprunt emprunt = ObtenirEmpruntActifParLivre(idLivre);
            return (emprunt!=null);
        }

        public Auteur ObtenirAuteur(int id)
        {
            Auteur auteurTrouve = bdd.Auteurs.FirstOrDefault(auteur => auteur.Id == id);
            return auteurTrouve;
        }

        public Client ObtenirClient(string email)
        {
            Client clientTrouve = bdd.Clients.FirstOrDefault(client => client.Email == email);
            return clientTrouve;
        }

        public List<Emprunt> ObtenirEmpruntsActifParClient(string email)
        {
            Client client = ObtenirClient(email);
            List<Emprunt> liste = bdd.Emprunts.FindAll(emprunt => emprunt.Client.Email == email && emprunt.DateRetour is null);
            return liste;
        }

        public Livre ObtenirLivre(int id)
        {
            Livre livreTrouve= bdd.Livres.FirstOrDefault(livre => livre.Id==id);
            return livreTrouve;
        }

        public List<Emprunt> ObtenirTousEmprunts()
        {
            return bdd.Emprunts;
        }

        public List<Auteur> ObtenirTousLesAuteurs()
        {
            return bdd.Auteurs;
        }

        public List<Client> ObtenirTousLesClients()
        {
            return bdd.Clients;
        }

        public List<Livre> ObtenirTousLesLivres()
        {
            return bdd.Livres;
        }

        public List<Auteur> RechercherAuteurs(string texte)
        {
            string texteUpper = texte.ToUpper();
            return bdd.Auteurs.FindAll(auteur => auteur.Nom.ToUpper().Contains(texteUpper));
        }

        public List<Livre> RechercherLivres(string texte)
        {
            string texteUpper = texte.ToUpper();
            return bdd.Livres.FindAll(livre => livre.Titre.ToUpper().Contains(texteUpper));
        }

        public List<Livre> ObtenirLivresParAuteur(int idAuteur)
        {
            return bdd.Livres.FindAll(livre => livre.Auteur.Id ==idAuteur);
        }

        public Emprunt ObtenirEmpruntActifParLivre(int idLivre)
        {
            Emprunt empruntTrouve = bdd.Emprunts.FirstOrDefault(emprunt => emprunt.Livre.Id == idLivre && emprunt.DateRetour is null);
            return empruntTrouve;
        }
    }
}