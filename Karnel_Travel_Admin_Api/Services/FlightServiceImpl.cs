using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class FlightServiceImpl : IFlightService
{
    private DatabaseContext db;
    private IAirportService airportService;
    public FlightServiceImpl(DatabaseContext _db, IAirportService _airportService)
    {
        db = _db;
        airportService = _airportService;
    }

    public bool create(Flight flight)
    {
        if (flight.DepartureAirportId == flight.ArrivalAirportId)
        {
            return false;
        }
        if (flight.DepartureTime > flight.ArrivalTime)
        {
            return false;
        }

        if (airportService.findById(flight.ArrivalAirportId) == null || airportService.findById(flight.DepartureAirportId) == null)
        {
            return false;
        }
        try
        {
            db.Flights.Add(flight);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool delete(int id)
    {
        try
        {
            var flight = db.Flights.Find(id);
            if (flight == null)
            {
                return false;
            }
            flight.IsHide = true;
            return update(flight);
        }
        catch
        {
            return false;
        }
    }

    public bool update(Flight flight)
    {
        try
        {
            db.Entry(flight).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public List<FlightDTO> findAllDeleted()
    {

        return db.Flights.Where(f => f.IsHide == true).Select(f => new FlightDTO
        {
            FlightId = f.FlightId,
            DepartureAirportId = f.DepartureAirportId,
            ArrivalAirportId = f.ArrivalAirportId,
            StartDate = f.StartDate,
            DepartureTime = f.DepartureTime,
            ArrivalTime = f.ArrivalTime,
            FlightPrice = f.FlightPrice,
            IsHide = f.IsHide
        }).ToList();
    }

    public List<FlightDTO> findAllDTO()
    {
        return db.Flights.Where(f => f.IsHide == false).Select(f => new FlightDTO
        {
            FlightId = f.FlightId,
            DepartureAirportId = f.DepartureAirportId,
            ArrivalAirportId = f.ArrivalAirportId,
            StartDate = f.StartDate,
            DepartureTime = f.DepartureTime,
            ArrivalTime = f.ArrivalTime,
            FlightPrice = f.FlightPrice,
            IsHide = f.IsHide
        }).ToList();
    }

    public FlightDTO findByIdDTO(int id)
    {
        return findAllDTO().SingleOrDefault(f => f.FlightId == id);
    }

    public bool Recover(int id)
    {

        var flight = db.Flights.SingleOrDefault(f => f.FlightId == id && f.IsHide == true);
        if (flight == null)
        {
            return false;
        }
        flight.IsHide = false;
        return update(flight);

    }


}
