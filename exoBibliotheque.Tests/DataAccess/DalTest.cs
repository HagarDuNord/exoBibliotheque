using System;
using System.Collections.Generic;
using exoBibliotheque.Models;
using exoBibliotheque.Models.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exoBibliotheque.Tests.DataAccess
{
    [TestClass]
    public class DalTest
    {
        Dal dal;
        static string EMAIL_CLIENT_INCONNU = "cestquicetype@inconnu.com"; // Email pas dans le JDD
        [TestInitialize]
        public void Initialize()
        {
            dal = new Dal(BddBouchon.Instance);
        }

        [TestMethod]
        public void ExistClient_Connu()
        {
            // Vérifie qu'un client du JDD est bien présent
            bool clientTrouve = dal.ClientExiste("kaiser.franck@bayern.de");
            Assert.IsTrue(clientTrouve);
        }

        [TestMethod]
        public void ExistClient_Inconnu()
        {
            // Vérifie qu'un client totalement bidon n'existe pas
            bool clientTrouve = dal.ClientExiste(EMAIL_CLIENT_INCONNU);
            Assert.IsFalse(clientTrouve);
        }

        [TestMethod]
        public void CreerClient_NouveauClient()
        {
            string email = "nouveau.client1@mail.com";
            string nom = "Nom Nouveau Client1";
            Client nouveauClient = dal.CreerClient(email, nom);
            // Vérifie que client retourné correspond aux valeurs attendus
            Assert.IsNotNull(nouveauClient);
            Assert.AreEqual(email, nouveauClient.Email);
            Assert.AreEqual(nom, nouveauClient.Nom);
            // Vérifie que le client existe en base
            bool clientTrouve = dal.ClientExiste(email);
            Assert.IsTrue(clientTrouve);
        }

        [TestMethod]
        public void CreerClient_DejaClient()
        {
            string email = "nouveau.client2@mail.com";
            string nom1 = "Nom Nouveau Client2";
            Client nouveauClient1 = dal.CreerClient(email, nom1);
            Assert.IsNotNull(nouveauClient1);
            Assert.AreEqual(email, nouveauClient1.Email);
            Assert.AreEqual(nom1, nouveauClient1.Nom);

            string nom2 = "Nom2 Nouveau Client2";
            Client nouveauClient2 = dal.CreerClient(email, nom2);
            // La création d'un second client avec le même email doit retourner Null
            Assert.IsNull(nouveauClient2);

        }
        [TestMethod]
        public void CreerEmprunt_LivreLibre_ClientNbEmpruntOK()
        {
            //Vérifie que l'emprunt d'un livre libre pour un client qui n'a pas atteint son nombre d'emprunt max est OK
            int idLivre = 4; // Notre-Dame de Paris
            string email = "client.emprunt1@mail.com";
            string nom = "Nom Client Emprunt OK";
            Client nouveauClient1 = dal.CreerClient(email, nom);
            Emprunt emprunt = dal.CreerEmprunt(idLivre, email, DateTime.Now);
            Assert.IsNotNull(emprunt);
        }

        [TestMethod]
        public void CreerEmprunt_LivreEmprunte_ClientNbEmpruntOK()
        {
            //Vérifie qu'on ne pas emprunter un livre deux fois
            int idLivre = 1; // Shinning déjà emprunté dans JDD
            //Vérifie que l'emprunt d'un livre libre pour un client qui n'a pas atteint son nombre d'emprunt max est OK
            string email = "client.emprunt1@mail.com";
            string nom = "Nom Client Emprunt OK";
            Client nouveauClient1 = dal.CreerClient(email, nom);
            Emprunt emprunt = dal.CreerEmprunt(idLivre, email, DateTime.Now);
            Assert.IsNull(emprunt);
        }

        [TestMethod]
        public void CreerEmprunt_LivreLibre_ClientNbEmpruntMax()
        {
            //Vérifie qu'on ne pas emprunter un livre deux fois
            int idLivre = 5; // Alice aux
            string email = "kaiser.franck@bayern.de";
            Emprunt emprunt = dal.CreerEmprunt(idLivre, email, DateTime.Now);
            Assert.IsNull(emprunt);
        }

        [TestMethod]
        public void ObtenirAuteur_IdConnu()
        {
            int idAuteur = 1; // Id de Stephen King
            Auteur auteur = dal.ObtenirAuteur(idAuteur);
            Assert.IsNotNull(auteur);
            Assert.AreEqual("Stephen King", auteur.Nom);

        }

        [TestMethod]
        public void ObtenirAuteur_IdInconnu()
        {
            int idAuteur = 0; // Id bidon
            Auteur auteur = dal.ObtenirAuteur(idAuteur);
            Assert.IsNull(auteur);

        }

        [TestMethod]
        public void ObtenirClient_EmailConnu()
        {
            string email = "kaiser.franck@bayern.de"; // Email de Frank Ribery
            Client client = dal.ObtenirClient(email);
            Assert.IsNotNull(client);
            Assert.AreEqual("Franck Ribery", client.Nom);

        }

        [TestMethod]
        public void ObtenirClient_EmailInconnu()
        {
            
            Client client = dal.ObtenirClient(EMAIL_CLIENT_INCONNU);
            Assert.IsNull(client);

        }

        [TestMethod]
        public void ObtenirEmpruntsActifParClient_ListeRenseigne()
        {
            string email = "kaiser.franck@bayern.de"; // Email de Frank Ribery
            List<Emprunt> liste = dal.ObtenirEmpruntsActifParClient(email);
            Assert.IsNotNull(liste);
            Assert.AreEqual(2, liste.Count);
        }

        [TestMethod]
        public void ObtenirEmpruntsActifParClient_ListeVide()
        {
            // Creation d'un client => Il n'a encore rien emprunté
            string email = "client.liste.emprunt@mail.com";
            string nom = "Nom Client Liste Emprunt";
            Client nouveauClient1 = dal.CreerClient(email, nom);
            
            List<Emprunt> liste = dal.ObtenirEmpruntsActifParClient(email);
            // On vérifie que le système retourne une liste vide
            Assert.IsNotNull(liste);
            Assert.AreEqual(0, liste.Count);
        }

        [TestMethod]
        public void ObtenirEmpruntsActifParClient_ClientInconu()
        {
            // Client inconnu => Il n'a encore rien emprunté
            List<Emprunt> liste = dal.ObtenirEmpruntsActifParClient(EMAIL_CLIENT_INCONNU);
            // On vérifie que le système retourne une liste vide
            Assert.IsNotNull(liste);
            Assert.AreEqual(0, liste.Count);
        }

        [TestMethod]
        public void ObtenirLivre_IdConnu()
        {
            int idLivre = 1; // "Shinning"
            Livre livre = dal.ObtenirLivre(idLivre);
            Assert.IsNotNull(livre);
            Assert.AreEqual("Shinning", livre.Titre);
            Assert.AreEqual(new DateTime(1977, 1, 28), livre.DateParution);
            Assert.AreEqual("Stephen King", livre.Auteur.Nom);
        }
    

        [TestMethod]
        public void ObtenirLivre_IdInconnu()
        {
            int idLivre = 0; // Id bidon
            Livre livre = dal.ObtenirLivre(idLivre);
            Assert.IsNull(livre);

        }

        [TestMethod]
        public void EmpruntActifExiste_LivreEmprunte()
        {
            int idLivre = 1; // Shinning emprunté dans le JDD
            bool empruntActif = dal.EmpruntActifParLivreExiste(idLivre);
            Assert.IsTrue(empruntActif);
        }

        [TestMethod]
        public void EmpruntActifExiste_LivreLibre()
        {
            int idLivre = 5; // Alice
            bool empruntActif = dal.EmpruntActifParLivreExiste(idLivre);
            Assert.IsFalse(empruntActif);
        }

        [TestMethod]
        public void EmpruntActifExiste_LivreInconnu()
        {
            int idLivre = 0; // Id Bidon Alice
            bool empruntActif = dal.EmpruntActifParLivreExiste(idLivre);
            Assert.IsFalse(empruntActif);
        }

        [TestMethod]
        public void RechercherAuteurs_Minuscule_2Resultats()
        {
            string texte = "g"; // Stephen King & Victor Hugo
            List<Auteur> liste = dal.RechercherAuteurs(texte);
            Assert.IsNotNull(liste);
            Assert.AreEqual(2, liste.Count);
        }

        [TestMethod]
        public void RechercherAuteurs_Majuscule_2Resultats()
        {
            string texte = "S"; // Stephen King & Lewis Carroll
            List<Auteur> liste = dal.RechercherAuteurs(texte);
            Assert.IsNotNull(liste);
            Assert.AreEqual(2, liste.Count);
        }

        [TestMethod]
        public void RechercherAuteurs_0Resultat()
        {
            string texte = "Zazertio"; 
            List<Auteur> liste = dal.RechercherAuteurs(texte);
            Assert.IsNotNull(liste);
            Assert.AreEqual(0, liste.Count);
        }

        [TestMethod]
        public void RechercherLivres_Minuscule_2Resultats()
        {
            string texte = "is"; //Misery && Notre-Dame de Paris
            List<Livre> liste = dal.RechercherLivres(texte);
            Assert.IsNotNull(liste);
            Assert.AreEqual(2, liste.Count);
        }

        [TestMethod]
        public void RechercherLivres_Majuscule_3Resultats()
        {
            string texte = "P"; // Docteur Sleep & Notre-Dame de Paris & Les Aventures d’Alice au pays des merveilles
            List<Livre> liste = dal.RechercherLivres(texte);
            Assert.IsNotNull(liste);
            Assert.AreEqual(3, liste.Count);
        }

        [TestMethod]
        public void RechercherLivres_0Resultat()
        {
            string texte = "Zazertio";
            List<Livre> liste = dal.RechercherLivres(texte);
            Assert.IsNotNull(liste);
            Assert.AreEqual(0, liste.Count);
        }

        [TestMethod]
        public void ObtenirEmpruntActifParLivre_LivreEmprunte()
        {
            int idLivre = 1; // Shinning emprunté dans le JDD par Ribery
            Emprunt emprunt = dal.ObtenirEmpruntActifParLivre(idLivre);
            Assert.IsNotNull(emprunt);
            Assert.AreEqual(emprunt.Client.Email, "kaiser.franck@bayern.de");
        }

        [TestMethod]
        public void ObtenirEmpruntActifParLivre_LivreLibre()
        {
            int idLivre = 5; // Alice non emprunté dans le JDD
            Emprunt emprunt = dal.ObtenirEmpruntActifParLivre(idLivre);
            Assert.IsNull(emprunt);
        }

        [TestMethod]
        public void ObtenirEmpruntActifParLivre_LivreInconnu()
        {
            int idLivre = 0; // Id bidon
            Emprunt emprunt = dal.ObtenirEmpruntActifParLivre(idLivre);
            Assert.IsNull(emprunt);
        }
    }
}
