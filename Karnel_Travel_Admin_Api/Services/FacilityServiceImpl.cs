using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class FacilityServiceImpl : IFacilityService
{
    private DatabaseContext db;
    public FacilityServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public bool create(Facility facility)
    {
        try
        {
            db.Facilities.Add(facility);
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
            var facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return false;
            }
            facility.IsHide = true;
            return update(facility);
        }
        catch
        {
            return false;
        }
    }

    public bool update(Facility facility)
    {
        try
        {
            db.Entry(facility).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public List<FacilityDTO> findAll()
    {
        return db.Facilities.Where(f => f.IsHide == false).Select(f => new FacilityDTO { FacilityId = f.FacilityId, FacilityName = f.FacilityName, IsHide = f.IsHide }).ToList();
    }

    public List<FacilityDTO> findAllDeleted()
    {
        return db.Facilities.Where(f => f.IsHide == true).Select(f => new FacilityDTO { FacilityId = f.FacilityId, FacilityName = f.FacilityName, IsHide = f.IsHide }).ToList();
    }

    public FacilityDTO findByIdDTO(int id)
    {
        return db.Facilities.Select(f => new FacilityDTO { FacilityId = f.FacilityId, FacilityName = f.FacilityName, IsHide = f.IsHide }).SingleOrDefault(f => f.IsHide == false && f.FacilityId == id);
    }

    public bool Recover(int id)
    {

        var facility = db.Facilities.SingleOrDefault(a => a.FacilityId == id && a.IsHide == true);
        if (facility == null)
        {
            return false;
        }
        facility.IsHide = false;
        return update(facility);

    }

    public Facility findById(int id)
    {
        return db.Facilities.SingleOrDefault(f => f.FacilityId == id && f.IsHide == false);
    }
	public List<FacilityDTO> findAll(int hotelId)
	{
		var hotel = db.Hotels.SingleOrDefault(h => h.HotelId == hotelId && h.IsHide == false);
		return hotel.Facilities.Select(f => new FacilityDTO
		{
			FacilityId = f.FacilityId,
			FacilityName = f.FacilityName
		}).ToList();

	}
}
