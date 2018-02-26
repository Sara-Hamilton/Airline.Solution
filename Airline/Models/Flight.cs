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

    public override bool Equals(System.Object otherFlight)
    {
      if (!(otherFlight is Flight))
      {
        return false;
      }
      else
      {
        Flight newFlight = (Flight) otherFlight;
        bool idEquality = (this.GetId() == newFlight.GetId());
        bool departureTimeEquality = (this.GetDepartureTime() == newFlight.GetDepartureTime());
        bool arrivalTimeEquality = (this.GetArrivalTime() == newFlight.GetArrivalTime());
        bool statusEquality = (this.GetStatus() == newFlight.GetStatus());
        return (idEquality && departureTimeEquality && arrivalTimeEquality && statusEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetDepartureTime().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetDepartureTime()
    {
      return _departureTime;
    }

    public void SetDepartureTime(string departureTime)
    {
      _departureTime = departureTime;
    }

    public string GetArrivalTime()
    {
      return _arrivalTime;
    }

    public void SetArrivalTime(string arrivalTime)
    {
      _arrivalTime = arrivalTime;
    }

    public string GetStatus()
    {
      return _status;
    }

    public void SetStatus(string status)
    {
      _status = status;
    }
  }

}
