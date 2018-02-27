using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Airline.Models;

namespace Airline.Tests
{
  [TestClass]
  public class FlightTest : IDisposable
  {
    public void Dispose()
    {
      Flight.DeleteAll();
      City.DeleteAll();
    }

    public void ItemTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Flight.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    //need test for delete
  }
}
