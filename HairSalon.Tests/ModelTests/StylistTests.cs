using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=faiza_husain_test;";
        }
        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            int result = Stylist.GetAll().Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_IfSameNameReturnTrue_Stylist()
        {
            Stylist firstStylist = new Stylist("Stefi");
            Stylist secondStylist = new Stylist("Stefi");

            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            Stylist testStylist = new Stylist("Fizza");

            testStylist.Save();
            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist>{testStylist};

            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToStylist_Id()
        {
            Stylist testStylist = new Stylist("Arya");

            testStylist.Save();
            Stylist savedStylist = Stylist.GetAll()[0];

            int result = savedStylist.GetId();
            int testId = testStylist.GetId();

            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsStylistInDatabase_Stylist()
        {
            Stylist testStylist = new Stylist("Arya");
            testStylist.Save();

            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            Assert.AreEqual(testStylist, foundStylist);
        }

        [TestMethod]
        public void GetClients_GetAllStylistClients_ClientList()
        {
            Stylist testStylist = new Stylist("Arya");
            testStylist.Save();

            Client firstClient = new Client("Sansa", testStylist.GetId());
            firstClient.Save();

            Client secondClient = new Client("Jeyne", testStylist.GetId());
            secondClient.Save();

            List<Client> testClientList = new List<Client>{firstClient, secondClient};
            List<Client> resultClientList = testStylist.GetStylistClients();

            CollectionAssert.AreEqual(testClientList, resultClientList);
        }
    }
}
