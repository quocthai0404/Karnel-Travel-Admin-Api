using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class TourServiceImpl : ITourService
{
    private DatabaseContext db;
    public TourServiceImpl(DatabaseContext _db)
    {
        db = _db;

    }

    public bool create(Tour tour)
    {
        try
        {
            db.Tours.Add(tour);
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
            var tour = db.Tours.Find(id);
            if (tour == null)
            {
                return false;
            }
            tour.IsHide = true;
            return update(tour);
        }
        catch
        {
            return false;
        }
    }

    public List<TourDTO> findAllDeleted()
    {
        return db.Tours.Where(b => b.IsHide == true)
            .Select(b => new TourDTO {
                TourId = b.TourId,
                TourName = b.TourName,
                TourDescription = b.TourDescription,
                Departure = b.Departure,
                Arrival = b.Arrival,
                TourPrice = b.TourPrice,
                IsHide = b.IsHide
            }).ToList();
    }

    public List<TourDTOAndMainPhoto> findAllTour()
    {
        var tours = db.Tours.Where(t => t.IsHide == false);

        var TourDTOs = findAllTourDto();
        var list = TourDTOs.Select(tour =>
        {
            var record = new TourDTOAndMainPhoto()
            {
                TourId = tour.TourId,
                TourName = tour.TourName,
                TourDescription = tour.TourDescription,
                Departure = tour.Departure,
                Arrival = tour.Arrival,
                DepartureProvince = findProvinceName(tour.Departure),
                ArrivalProvince = findProvinceName(tour.Arrival),
                TourPrice = tour.TourPrice,
                IsHide = tour.IsHide,
            };

            var mainPhoto = findMainPhoto(tour.TourId);
            if (mainPhoto != null)
            {
                record.PhotoUrl = mainPhoto.PhotoUrl;
            }
            return record;
        }).ToList();
        return list;

    }

    public List<TourDTOAndMainPhoto> findAllTourDto()
    {
        return db.Tours.Where(t => t.IsHide == false)
        .Select(t => new TourDTOAndMainPhoto
        {
            TourId = t.TourId,
            TourName = t.TourName,
            TourDescription = t.TourDescription,
            Departure = t.Departure,
            Arrival = t.Arrival,
            TourPrice = t.TourPrice,
            IsHide = t.IsHide,
        }).ToList();
    }

    public TourDTOAndMainPhoto findById(int id)
    {
        var tour = db.Tours.FirstOrDefault(t => t.TourId == id && t.IsHide == false);
        var detail = new TourDTOAndMainPhoto
        {
            TourId = tour.TourId,
            TourName = tour.TourName,
            TourDescription = tour.TourDescription,
            Departure = tour.Departure,
            Arrival = tour.Arrival,
            DepartureProvince = findProvinceName(tour.Departure),
            ArrivalProvince = findProvinceName(tour.Arrival),
            TourPrice = tour.TourPrice,
            IsHide = tour.IsHide,
        };
        var mainPhoto = findMainPhoto(id);
        if (mainPhoto != null)
        {
            detail.PhotoUrl = mainPhoto.PhotoUrl;
        }
        return detail;
    }

    public Tour findByIdObject(int id)
    {
        return db.Tours.SingleOrDefault(b => b.TourId == id && b.IsHide == false);
    }

    public PhotoDTO findMainPhoto(int tourId)
    {
        return db.Photos
            .Where(p => p.TourId == tourId)
            .Select(p => new PhotoDTO { PhotoUrl = p.PhotoUrl })
            .FirstOrDefault();
    }

    public string findProvinceName(int provinceId)
    {
        return db.Provinces.SingleOrDefault(p => p.ProvinceId == provinceId).ProvinceName;
    }

    public bool Recover(int id)
    {
        var tour = db.Tours.SingleOrDefault(a => a.TourId == id && a.IsHide == true);
        if (tour == null)
        {
            return false;
        }
        tour.IsHide = false;
        return update(tour);
    }

    public bool update(Tour tour)
    {
        try
        {
            db.Entry(tour).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
