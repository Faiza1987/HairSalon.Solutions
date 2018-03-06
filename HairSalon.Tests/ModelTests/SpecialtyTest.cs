using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using HairSalon;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {

     public SpecialtyTests()
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
      int result = Specialty.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfSpecialtiesAreTheSame_Specialty()
    {
     Specialty newSpecialty = new Specialty("Men's Haircut");
     Specialty secondSpecialty = new Specialty("Men's Haircut");

      bool result = secondSpecialty.Equals(newSpecialty);

      Assert.AreEqual(true, result);
    } 
    [TestMethod]
    public void SaveSpecialty_AssignsIdToObject_Id()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Women's Haircut");

      //Act
      testSpecialty.Save();
      Specialty savedSpecialty = Specialty.GetAll()[0];

      int result = savedSpecialty.GetId();
      int testId = testSpecialty.GetId();
      Console.WriteLine(result);
      Console.WriteLine(testId);

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsSpecialtyInDataBase_Specialty()
    {
    //Arrange
    Specialty testSpecialty = new Specialty("Beard cut");
    testSpecialty.Save();

    //Act
    Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

    //Assert
    Assert.AreEqual(testSpecialty, foundSpecialty);
    }
    [TestMethod]
    public void Edit_UpdatesSpecialtyinDatabase_String()
    {
      //Arrange
      string firstSpecialtyName = "Beard shaping";
      Specialty testSpecialty = new Specialty(firstSpecialtyName);
      testSpecialty.Save();
      string secondSpecialtyName = "Beard cuts";

      //Act
      testSpecialty.UpdateName(secondSpecialtyName);
      string result = Specialty.Find(testSpecialty.GetId()).GetDescription();

      //Assert
      Assert.AreEqual(secondSpecialtyName, result);
    }
    [TestMethod]
    public void DeleteOneSpecialty_DeletesOneSpecialtyInDatabase_True()
    {
    //Arrange
    Specialty testSpecialty = new Specialty("Women's cuts");
    testSpecialty.Save();

    //Act
    testSpecialty.DeleteOne();
    int result = Specialty.GetAll().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }
    [TestMethod]
    public void DeleteAll_DeletesAllSpecialties_True()
    {
    //Arrange
    Specialty testSpecialty = new Specialty("Faiza");
    testSpecialty.Save();

    //Act
    Specialty.DeleteAll();
    int result = Specialty.GetAll().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }
  }
}

