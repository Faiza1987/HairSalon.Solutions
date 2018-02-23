using System;
using HairSalon.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=faiza_husain_test;";
        }
        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }
        [TestMethod]
        public void GetAllClients_DatabaseEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Client.GetAllClients().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Equals_OverrideTrueIfClientsAreIdentical_Client(string fullName, int phoneNumber, int stylistId, int clientId)
        {
            //Arrange, Act
            Client firstClient = new Client("Faiza", 1234567890, 1, 1);
            Client secondClient = new Client("Faiza", 1234567890, 1, 1);

            //Assert
            Assert.AreEqual(firstClient, secondClient);
        }
    }
}
