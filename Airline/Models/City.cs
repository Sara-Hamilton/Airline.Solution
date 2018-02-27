using System.Collections.Generic;
using System;
using Airline;
using MySql.Data.MySqlClient;

namespace Airline.Models
{
  public class City
  {
    private int _id;
    private string _name;

    public City(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }

    public override bool Equals(System.Object otherCity)
    {
      if (!(otherCity is City))
      {
        return false;
      }
      else
      {
        City newCity = (City) otherCity;
        return this.GetId().Equals(newCity.GetId());
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string name)
    {
      _name = name;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<City> GetAll()
    {
     List<City> allCities = new List<City> {};
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM cities;";
     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int CityId = rdr.GetInt32(0);
       string CityName = rdr.GetString(1);
       City newCity = new City(CityName, CityId);
       allCities.Add(newCity);
     }
     conn.Close();
     if (conn != null)
     {
         conn.Dispose();
     }
     return allCities;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cities;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static City Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int cityId = 0;
      string cityName = "";

      while(rdr.Read())
      {
        cityId = rdr.GetInt32(0);
        cityName = rdr.GetString(1);
      }
      City newCity = new City(cityName, cityId);
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return newCity;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("DELETE FROM cities WHERE id = @CityId; DELETE FROM cities_flights WHERE city_id = @CityId;", conn);

      MySqlParameter cityIdParameter = new MySqlParameter();
      cityIdParameter.ParameterName = "@CityId";
      cityIdParameter.Value = this.GetId();

      cmd.Parameters.Add(cityIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void AddFlight(Flight newFlight)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities_flights (city_id, flight_id) VALUES (@CityId, @FlightId);";

      MySqlParameter city_id = new MySqlParameter();
      city_id.ParameterName = "@CityId";
      city_id.Value = _id;
      cmd.Parameters.Add(city_id);

      MySqlParameter flight_id = new MySqlParameter();
      flight_id.ParameterName = "@FlightId";
      flight_id.Value = newFlight.GetId();
      cmd.Parameters.Add(flight_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public List<Flight> GetFlights()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT flights. * FROM cities
          JOIN cities_flights ON (cities.id = cities_flights.city_id)
          JOIN flights ON (cities_flights.flight_id = flights.id)
          WHERE cities.id = @CityId;";

      MySqlParameter cityIdParameter = new MySqlParameter();
      cityIdParameter.ParameterName = "@CityId";
      cityIdParameter.Value = _id;
      cmd.Parameters.Add(cityIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Flight> flights = new List<Flight>{};

      while(rdr.Read())
      {
        int flightId = rdr.GetInt32(0);
        string flightDepartureTime = rdr.GetString(1);
        string flightArrivalTime = rdr.GetString(2);
        string flightStatus = rdr.GetString(3);
        Flight newFlight = new Flight(flightDepartureTime, flightArrivalTime, flightStatus, flightId);
        flights.Add(newFlight);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return flights;
    }

  }
}
