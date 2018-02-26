using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airline.Models;
using System;
using System.Collections.Generic;

namespace Airline.Tests
{
  [TestClass]
  public class CityTest : IDisposable
 {
    public void CityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_test;";
    }

    public void Dispose()
    {
      // Flight.DeleteAll();
      City.DeleteAll();
    }

    // [TestMethod]
    //  public void GetAll_CitiesEmptyAtFirst_0()
    //  {
    //    //Arrange, Act
    //    int result = City.GetAll().Count;
    //
    //    //Assert
    //    Assert.AreEqual(0, result);
    //  }

    [TestMethod]
    public void Save_SavesCityToDatabase_CityList()
    {
      //Arrange
      City testCity = new City("Cleveland");
      testCity.Save();

      //Act
      List<City> result = City.GetAll();
      List<City> testList = new List<City>{testCity};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
     public void Save_DatabaseAssignsIdToCity_Id()
     {
       //Arrange
       City testCity = new City("Cleveland");
       testCity.Save();

       //Act
       City savedCity = City.GetAll()[0];

       int result = savedCity.GetId();
       int testId = testCity.GetId();

       //Assert
       Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsCityInDatabase_City()
    {
      //Arrange
      City testCity = new City("Cleveland");
      testCity.Save();

      //Act
      City foundCity = City.Find(testCity.GetId());

      //Assert
      Assert.AreEqual(testCity, foundCity);
    }

    [TestMethod]
    public void Delete_DeletesCityAssociationsFromDatabase_CityList()
    {
      //Arrange
      Flight testFlight = new Flight("3:00 PM", "8:47 PM", "On time");
      testFlight.Save();

      string testName = "Cleveland";
      City testCity = new City(testName);
      testCity.Save();

      //Act
      testCity.AddFlight(testFlight);
      testCity.Delete();

      List<City> resultFlightCities = testFlight.GetArrivalCities();
      List<City> testFlightCities = new List<City> {};

      //Assert
      CollectionAssert.AreEqual(testFlightCities, resultFlightCities);
    }

  }
}
