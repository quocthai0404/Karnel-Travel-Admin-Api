using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class AirportServiceImpl : IAirportService
{
    private DatabaseContext db;
    public AirportServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public bool create(Karnel_Travel_Admin_Api.Models.Airport airport)
    {
        try
        {
            db.Airports.Add(airport);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool delete(string airportId)
    {

        try
        {
            var airport = findById(airportId);
            if (airport == null)
            {
                return false;
            }
            airport.IsHide = true;
            return update(airport);
        }
        catch
        {
            return false;
        }
    }

    public bool update(Karnel_Travel_Admin_Api.Models.Airport airport)
    {
        try
        {
            db.Entry(airport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public Karnel_Travel_Admin_Api.Models.Airport findById(string airportId)
    {
        return db.Airports.SingleOrDefault(a => a.AirportId == airportId && a.IsHide == false);
    }

    public AirportDTO findByIdDTO(string airportId)
    {
        return db.Airports.Select(a => new AirportDTO { AirportId = a.AirportId, AirportName = a.AirportName, IsHide = a.IsHide }).SingleOrDefault(a => a.AirportId == airportId && a.IsHide == false);
    }

    public List<AirportDTO> findAll()
    {
        return db.Airports.Where(a => a.IsHide == false).Select(a => new AirportDTO { AirportId = a.AirportId, AirportName = a.AirportName, IsHide = a.IsHide }).ToList();
    }

    public List<AirportDTO> findAllDeleted()
    {
        return db.Airports.Where(a => a.IsHide == true).Select(a => new AirportDTO { AirportId = a.AirportId, AirportName = a.AirportName, IsHide = a.IsHide }).ToList();
    }

    public bool Recover(string airportId)
    {

        var airport = db.Airports.SingleOrDefault(a => a.AirportId == airportId && a.IsHide == true);
        if (airport == null)
        {
            return false;
        }
        airport.IsHide = false;
        return update(airport);

    }
}
