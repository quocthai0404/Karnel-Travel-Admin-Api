using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class RestaurantServiceImpl : IRestaurantService
{
    private DatabaseContext db;
    public RestaurantServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public bool create(Restaurant restaurant)
    {
        try
        {
            db.Restaurants.Add(restaurant);
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
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return false;
            }
            restaurant.IsHide = true;
            return update(restaurant);
        }
        catch
        {
            return false;
        }
    }

    public bool update(Restaurant restaurant)
    {
        try
        {
            db.Entry(restaurant).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public List<RestaurantDTO> findAllDeleted()
    {
        return db.Restaurants.Where(b => b.IsHide == true)
            .Select(b => new RestaurantDTO
            {
                RestaurantId = b.RestaurantId,
                RestaurantName = b.RestaurantName,
                RestaurantDescription = b.RestaurantDescription,
                RestaurantPriceRange = b.RestaurantPriceRange,
                RestaurantLocation = b.RestaurantLocation,
                LocationId = b.LocationId,
                IsHide = b.IsHide,
            })
            .ToList();
    }

    public List<RestaurantDTO> findAllDTO()
    {
        return db.Restaurants.Where(b => b.IsHide == false)
            .Select(b => new RestaurantDTO
            {
                RestaurantId = b.RestaurantId,
                RestaurantName = b.RestaurantName,
                RestaurantDescription = b.RestaurantDescription,
                RestaurantPriceRange = b.RestaurantPriceRange,
                RestaurantLocation = b.RestaurantLocation,
                LocationId = b.LocationId,
                IsHide = b.IsHide,
            })
            .ToList();
    }

    public Restaurant findById(int id)
    {
        return db.Restaurants.SingleOrDefault(b => b.RestaurantId == id && b.IsHide == false);
    }

    public RestaurantDTO findByIdDTO(int id)
    {
        return findAllDTO().Select(b => new RestaurantDTO
        {
            RestaurantId = b.RestaurantId,
            RestaurantName = b.RestaurantName,
            RestaurantDescription = b.RestaurantDescription,
            RestaurantPriceRange = b.RestaurantPriceRange,
            RestaurantLocation = b.RestaurantLocation,
            LocationId = b.LocationId,
            IsHide = b.IsHide,
        }).SingleOrDefault(b => b.RestaurantId == id);
    }

    public bool Recover(int id)
    {
        var restaurant = db.Restaurants.SingleOrDefault(a => a.RestaurantId == id && a.IsHide == true);
        if (restaurant == null)
        {
            return false;
        }
        restaurant.IsHide = false;
        return update(restaurant);
    }


}
