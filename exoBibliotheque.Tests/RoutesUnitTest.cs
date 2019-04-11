using System;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace exoBibliotheque.Tests
{
    [TestClass]
    public class RoutesUnitTest
    {
        private static RouteData DefinirUrl(string url)
        {
            Mock<System.Web.HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(mockContext.Object);
            return routeData;
        }

        [TestMethod]
        public void TestRoute_Afficher_ParDefaut()
        {
            RouteData routeData = DefinirUrl("~/Afficher");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Afficher", routeData.Values["controller"]);
            Assert.AreEqual("Livres", routeData.Values["action"]);

        }

        [TestMethod]
        public void TestRoute_Afficher_Livres()
        {
            RouteData routeData = DefinirUrl("~/Afficher/Livres");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Afficher", routeData.Values["controller"]);
            Assert.AreEqual("Livres", routeData.Values["action"]);

        }

        [TestMethod]
        public void TestRoute_Afficher_Livre()
        {
            RouteData routeData = DefinirUrl("~/Afficher/Livre/1");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Afficher", routeData.Values["controller"]);
            Assert.AreEqual("Livre", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["id"]);

        }

        [TestMethod]
        public void TestRoute_Afficher_Auteurs()
        {
            RouteData routeData = DefinirUrl("~/Afficher/Auteurs");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Afficher", routeData.Values["controller"]);
            Assert.AreEqual("Auteurs", routeData.Values["action"]);

        }

        [TestMethod]
        public void TestRoute_Afficher_Auteur()
        {
            RouteData routeData = DefinirUrl("~/Afficher/Auteur/5");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Afficher", routeData.Values["controller"]);
            Assert.AreEqual("Auteur", routeData.Values["action"]);
            Assert.AreEqual("5", routeData.Values["id"]);

        }

        [TestMethod]
        public void TestRoute_Rechercher_Livre()
        {
            RouteData routeData = DefinirUrl("~/Rechercher/Livre/shi");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Rechercher", routeData.Values["controller"]);
            Assert.AreEqual("Livre", routeData.Values["action"]);
            Assert.AreEqual("shi", routeData.Values["texteCherche"]);

        }

        [TestMethod]
        public void TestRoute_Rechercher_Auteur()
        {
            RouteData routeData = DefinirUrl("~/Rechercher/Auteur/hugo");

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Rechercher", routeData.Values["controller"]);
            Assert.AreEqual("Auteur", routeData.Values["action"]);
            Assert.AreEqual("hugo", routeData.Values["texteCherche"]);

        }
    }
}
