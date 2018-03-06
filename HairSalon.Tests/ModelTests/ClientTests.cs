using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using HairSalon;
using System;

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
      Specialty.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfClientsAreTheSame_Client()
    {
      Client newClient = new Client("Faiza", 1);
      Client secondClient = new Client("Faiza", 1);

      bool result = secondClient.Equals(newClient);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void SaveClient_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("Sanya", 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();
      Console.WriteLine(result);
      Console.WriteLine(testId);

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void Find_FindsClientInDataBase_Client()
    {
      //Arrange
      Client testClient = new Client("Faiza");
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);  
    }
    [TestMethod]
    public void Edit_UpdatesClientinDatabase_String()
    {
      //Arrange
      string firstClientName = "Ahsan";
      Client testClient = new Client(firstClientName);
      testClient.Save();
      string secondClientName = "Ahsan Farooq";

      //Act
      testClient.UpdateName(secondClientName);
      string result = Client.Find(testClient.GetId()).GetName();

      //Assert
      Assert.AreEqual(secondClientName, result);
    }
    [TestMethod]
    public void DeleteOneClient_DeletesOneClientInDatabase_True()
    {
    //Arrange
    Client testClient = new Client("Faiza");
    testClient.Save();

    //Act
    testClient.DeleteOne();
    int result = Client.GetAll().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }
    [TestMethod]
    public void DeleteAll_DeletesAllClients_True()
    {
    //Arrange
    Client testClient = new Client("Faiza");
    testClient.Save();

    //Act
    Client.DeleteAll();
    int result = Client.GetAll().Count;

    //Assert
    Assert.AreEqual(result, 0);
    }
  }
}
