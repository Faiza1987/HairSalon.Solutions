using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon;
using System;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistsTest : IDisposable
  {

     public StylistsTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=faiza_husain_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfClientsAreTheSame_Client()
    {
      Stylist newStylist = new Stylist("Ahsan", 1);
      Stylist secondStylist = new Stylist("Ahsan", 1);

      bool result = secondStylist.Equals(newStylist);

      Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase__StylistList()
    {
      Stylist firstStylist = new Stylist("Arya");
      firstStylist.Save();
      Stylist secondStylist = new Stylist("Mehreen");
      secondStylist.Save();

      List<Stylist> expected = new List<Stylist>{secondStylist, firstStylist};

      List<Stylist> actual = Stylist.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Stylist testStylist = new Stylist("Mehreen");
      testStylist.Save();

      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Mehreen");
      testStylist.Save();

      Stylist result = Stylist.Find(testStylist.GetId());

      Assert.AreEqual(testStylist, result);
    }

    [TestMethod]
    public void DeleteThis_DeleteSpecificStylist_StylistList()
    {
      //Assign
      Stylist firstStylist = new Stylist("Mehreen");
      firstStylist.Save();
      Stylist secondStylist = new Stylist("Arya");
      secondStylist.Save();
      List<Stylist> expected = new List<Stylist>{secondStylist};

      //Act
      firstStylist.DeleteThis();
      List<Stylist> actual = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetClients_ReturnAllClientsByStylist_ClientList()
    {
      //Assign
      Stylist firstStylist = new Stylist("Faiza");
      firstStylist.Save();
      Client firstClient = new Client("Aurangzeb", firstStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Ali", firstStylist.GetId());
      secondClient.Save();
      List<Client> expected = new List<Client>{firstClient, secondClient};

      //Act
      List<Client> result = firstStylist.GetAllClients();

      //Assert
      CollectionAssert.AreEqual(expected, result);
    }
  }
}
