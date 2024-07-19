using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class HotelServiceImpl : IHotelService
{
    private DatabaseContext db;
    private IFacilityService facilityService;
    public HotelServiceImpl(DatabaseContext _db, IFacilityService  _facilityService)
    {
        db = _db;
        facilityService = _facilityService;
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
	public List<ReviewDto> findAllReview(int hotelId)
	{
		return (from review in db.Reviews
				join user in db.Users on review.UserId equals user.UserId
				where review.HotelId == hotelId && review.IsHide == false
				select new ReviewDto
				{
					ReviewId = review.ReviewId,
					ReviewStar = review.ReviewStar,
					ReviewText = review.ReviewText,
					UserId = review.UserId,
					UserFullName = user.Fullname,
					HotelId = review.HotelId
				}).ToList();
	}

	public List<HotelAndMainPhotoDto> listHotel()
	{



        var hotelDTOs = findAllDTO();



		var hotelList = hotelDTOs.Select(hotelDTO =>
		{
			var reviews = findAllReview(hotelDTO.HotelId);
			var countReview = reviews.Count();
			var totalStar = getSumOfReviewStars(hotelDTO.HotelId);
			double star = 0;
			if (countReview != 0)
			{
				star = (double)totalStar / (double)countReview;
			}
			var hotelAndMainPhoto = new HotelAndMainPhotoDto()
			{
				HotelId = hotelDTO.HotelId,
				HotelName = hotelDTO.HotelName,
				HotelDescription = hotelDTO.HotelDescription,
				HotelPriceRange = hotelDTO.HotelPriceRange,
				HotelLocation = hotelDTO.HotelLocation,
				LocationId = hotelDTO.LocationId,
				IsHide = hotelDTO.IsHide,
				countReview = countReview,
				totalStar = totalStar,
				star = Math.Round(star, 1),
				facilities = facilityService.findAll(hotelDTO.HotelId)
			};

			var mainPhoto = findMainPhoto(hotelDTO.HotelId);
			if (mainPhoto != null)
			{
				hotelAndMainPhoto.PhotoUrl = mainPhoto.PhotoUrl;
			}

			return hotelAndMainPhoto;
		}).ToList();

		return hotelList;
	}
	public int getSumOfReviewStars(int hotelId)
	{
		return findAllReview(hotelId).Sum(r => r.ReviewStar);
	}
	public PhotoDTO findMainPhoto(int hotelId)
	{
		return db.Photos
			.Where(p => p.HotelId == hotelId)
			.Select(p => new PhotoDTO { PhotoUrl = p.PhotoUrl })
			.FirstOrDefault();
	}

}
