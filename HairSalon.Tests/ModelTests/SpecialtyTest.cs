// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System.Collections.Generic;
// using HairSalon.Models;
// using HairSalon;
// using System;
//
// namespace HairSalon.Tests
// {
//   [TestClass]
//   public class SpecialtyTests : IDisposable
//   {
//
//      public SpecialtyTests()
//     {
//       DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=faiza_husain_test;";
//     }
//
//     public void Dispose()
//     {
//       Client.DeleteAll();
//       Stylist.DeleteAll();
//       Specialty.DeleteAll();
//     }
//
//     [TestMethod]
//     public void GetAll_SpecialtiesEmptyAtFirst_0()
//     {
//        int result = Specialty.GetAll().Count;
//        Assert.AreEqual(0, result);
//     }
//     [TestMethod]
//     public void Equals_ReturnsTrueForSameDescription_Specialty()
//     {
//         Specialty firstSpecialty = new Specialty("Men's Haircut");
//         Specialty secondSpecialty = new Specialty("Men's Haircut");
//         Assert.AreEqual(firstSpecialty, secondSpecialty);
//     }
//     [TestMethod]
//     public void Save_AddStylistToSpecialty_StylistList()
//     {
//         Stylist testStylist = new Stylist("Faiza");
//         testStylist.Save();
//
//         Specialty testSpecialty = new Specialty("Men's Haircut");
//         testSpecialty.Save();
//
//         testSpecialty.AddStylist(testStylist);
//
//         List<Stylist> result = testSpecialty.GetStylists();
//         List<Stylist> testList = new List<Stylist>{testStylist};
//
//         CollectionAssert.AreEqual(testList, result);
//     }
//
//     [TestMethod]
//     public void Find_FindsSpecialty_Specialty()
//     {
//         Specialty testSpecialty = new Specialty("Men's Haircut");
//         testSpecialty.Save();
//
//         Specialty result = Specialty.Find(testSpecialty.GetId());
//         Assert.AreEqual(result, testSpecialty);
//     }
//   }
// }
