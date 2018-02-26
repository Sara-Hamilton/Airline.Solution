using System.Collections.Generic;
using System;
using Airline;
using MySql.Data.MySqlClient;

namespace Airline.Models
{
  public class Flight
  {
    private int _id;
    private string _departureTime;
    private string _arrivalTime;
    private string _status;

    public Flight(string departureTime, string arrivalTime, string status, int id = 0)
    {
      _departureTime = departureTime;
      _arrivalTime = arrivalTime;
      _status = status;
      _id = id;
    }

    public string GetDepartureTime()
    {
      return _departureTime;
    }

    public void SetDepartureTime(string departureTime)
    {
      return departureTime;
    }

    public string GetArrivalTime()
    {
      return _arrivalTime;
    }

    public void SetArrivalTime(string arrivalTime)
    {
      return arrivalTime;
    }

    public string GetStatus()
    {
      return _status;
    }

    public string SetStatus(string status)
    {
      return status;
    }
  }

}
