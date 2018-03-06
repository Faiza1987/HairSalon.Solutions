using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using HairSalon;
using System;

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
      Specialty.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfStylistsAreTheSame_Stylist()
    {
      Stylist newStylist = new Stylist("Mehreen", 1);
      Stylist secondStylist = new Stylist("Mehreen", 1);

      bool result = secondStylist.Equals(newStylist);

      Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void SaveStylist_AssignsIdToObject_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Mehreen");

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();
      Console.WriteLine(result);
      Console.WriteLine(testId);

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsStylistInDataBase_Stylist()
    {
    //Arrange
    Stylist testStylist = new Stylist("Ali");
    testStylist.Save();

    //Act
    Stylist foundStylist = Stylist.Find(testStylist.GetId());

    //Assert
    Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void Edit_UpdatesStylistsinDatabase_String()
    {
      //Arrange
      string firstStylistName = "Ahsan";
      Stylist testStylist = new Stylist(firstStylistName);
      testStylist.Save();
      string secondStylistName = "Ahsan Farooq";

      //Act
      testStylist.UpdateName(secondStylistName);
      string result = Stylist.Find(testStylist.GetId()).GetName();

      //Assert
      Assert.AreEqual(secondStylistName, result);
    }
    [TestMethod]
    public void DeleteOneStylist_DeletesOneStylistInDatabase_True()
    {
    //Arrange
    Stylist testStylist = new Stylist("Faiza");
    testStylist.Save();

    //Act
    testStylist.DeleteOne();
    int result = Stylist.GetAll().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }
    [TestMethod]
    public void DeleteAll_DeletesAllStylists_True()
    {
    //Arrange
    Stylist testStylist = new Stylist("Faiza");
    testStylist.Save();

    //Act
    Stylist.DeleteAll();
    int result = Stylist.GetAll().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }
  }
}