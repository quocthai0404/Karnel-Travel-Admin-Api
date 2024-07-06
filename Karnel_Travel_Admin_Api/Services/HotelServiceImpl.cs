using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class HotelServiceImpl : IHotelService
{
    private DatabaseContext db;
    public HotelServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public bool create(Hotel hotel)
    {
        try
        {
            db.Hotels.Add(hotel);
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
            var hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return false;
            }
            hotel.IsHide = true;
            return update(hotel);
        }
        catch
        {
            return false;
        }
    }

    public bool update(Hotel hotel)
    {
        try
        {
            db.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public List<HotelDTO> findAllDeleted()
    {
        return db.Hotels.Where(b => b.IsHide == true).Select(b => new HotelDTO
        {
            HotelId = b.HotelId,
            HotelName = b.HotelName,
            HotelDescription = b.HotelDescription,
            HotelPriceRange = b.HotelPriceRange,
            HotelLocation = b.HotelLocation,
            LocationId = b.LocationId,
            IsHide = b.IsHide
        }).ToList();
    }

    public List<HotelDTO> findAllDTO()
    {
        return db.Hotels.Where(b => b.IsHide == false).Select(b => new HotelDTO
        {
            HotelId = b.HotelId,
            HotelName = b.HotelName,
            HotelDescription = b.HotelDescription,
            HotelPriceRange = b.HotelPriceRange,
            HotelLocation = b.HotelLocation,
            LocationId = b.LocationId,
            IsHide = b.IsHide
        }).ToList();
    }

    public HotelDTO findByIdDTO(int id)
    {
        return findAllDTO().Select(b => new HotelDTO
        {
            HotelId = b.HotelId,
            HotelName = b.HotelName,
            HotelDescription = b.HotelDescription,
            HotelPriceRange = b.HotelPriceRange,
            HotelLocation = b.HotelLocation,
            LocationId = b.LocationId,
            IsHide = b.IsHide
        }).SingleOrDefault(b => b.HotelId == id);
    }

    public bool Recover(int id)
    {

        var hotel = db.Hotels.SingleOrDefault(a => a.HotelId == id && a.IsHide == true);
        if (hotel == null)
        {
            return false;
        }
        hotel.IsHide = false;
        return update(hotel);

    }

    public Hotel findById(int id)
    {
        return db.Hotels.SingleOrDefault(a => a.HotelId == id && a.IsHide == false);
    }
}
