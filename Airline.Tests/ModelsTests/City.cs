using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airline.Models;
using System;

namespace Airline.Models.Tests
{
  [TestClass]
  public class AirlineModelTest : IDisposable
 {
    public AirlineModelTest()
    {
      Console.WriteLine("The port number and database name probably need to be changed");
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airline_test;";
    }

    public void Dispose()
    {
      //Delete everything from the database
    }

    [TestMethod]
    public void Test_JustATest_String()
    {
      Assert.AreEqual("this is a string from the model", AirlineModel.GetString());
    }
  }
}
