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
         //Arrange, Act
         int result = Stylist.GetAll().Count;

         //Assert
         Assert.AreEqual(0, result);
       }

       [TestMethod]
       public void Save_SavesToDatabase_StylistList()
       {
         //Arrange
         Stylist testStylist = new Stylist("Arya");

         //Act
         testStylist.Save();
         List<Stylist> result = Stylist.GetAll();
         List<Stylist> testList = new List<Stylist>{testStylist};

         //Assert
         CollectionAssert.AreEqual(testList, result);
       }

      [TestMethod]
      public void Equals_ReturnsTrueForSameName_Stylist()
      {
        //Arrange, Act
        Stylist firstStylist = new Stylist("Arya");
        Stylist secondStylist = new Stylist("Arya");

        //Assert
        Assert.AreEqual(firstStylist, secondStylist);
      }

     [TestMethod]
     public void Save_DatabaseAssignsIdToStylist_Id()
     {
       //Arrange
       Stylist testStylist = new Stylist("Arya");

       //Act
       testStylist.Save();
       Stylist savedStylist = Stylist.GetAll()[0];

       int result = savedStylist.GetId();
       int testId = testStylist.GetId();

       //Assert
       Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Arya");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylist_ClientList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Arya");
      testStylist.Save();

      Client firstClient = new Client("Sanna", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Rema", testStylist.GetId());
      secondClient.Save();

      //Act
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetStylistClients();

      //Assert
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

  }
}
