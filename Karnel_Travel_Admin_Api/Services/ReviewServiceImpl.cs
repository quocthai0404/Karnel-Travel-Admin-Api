using CloudinaryDotNet;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Services;

public class ReviewServiceImpl : IReviewService
{
    private DatabaseContext db;
    public ReviewServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public bool create(Review review)
    {
        try
        {
            db.Reviews.Add(review);
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
            db.Reviews.Remove(db.Reviews.Find(id));
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public List<ReviewDTO> findAllDeleted()
    {
        return db.Reviews.Where(r => r.IsHide == true)
            .Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                ReviewStar = r.ReviewStar,
                ReviewText = r.ReviewText,
                UserId = r.UserId,
                HotelId = r.HotelId,
                RestaurantId = r.RestaurantId,
                IsHide = r.IsHide
            }).ToList();
    }

    public List<ReviewDTO> findAllDTO()
    {
        return db.Reviews.Where(r => r.IsHide == false)
            .Select(r => new ReviewDTO {
                ReviewId = r.ReviewId,
                ReviewStar = r.ReviewStar,
                ReviewText = r.ReviewText,
                UserId = r.UserId,
                HotelId = r.HotelId,
                RestaurantId = r.RestaurantId,
                IsHide = r.IsHide
            }).ToList();
    }

    public Review findById(int id)
    {
        return db.Reviews.SingleOrDefault(b => b.ReviewId == id && b.IsHide == false);
    }

    public ReviewDTO findByIdDTO(int id)
    {
        return findAllDTO().Select(r => new ReviewDTO
        {
            ReviewId = r.ReviewId,
            ReviewStar = r.ReviewStar,
            ReviewText = r.ReviewText,
            UserId = r.UserId,
            HotelId = r.HotelId,
            RestaurantId = r.RestaurantId,
            IsHide = r.IsHide
        }).SingleOrDefault(b => b.ReviewId == id);
    }

    public bool Hide(int id)
    {
        try
        {
            var review = db.Reviews.Find(id);
            if (review == null)
            {
                return false;
            }
            review.IsHide = true;
            return update(review);
        }
        catch
        {
            return false;
        }
    }

    public bool UnHide(int id)
    {
        var review = db.Reviews.SingleOrDefault(a => a.ReviewId == id && a.IsHide == true);
        if (review == null)
        {
            return false;
        }
        review.IsHide = false;
        return update(review);
    }

    public bool update(Review review)
    {
        try
        {
            db.Entry(review).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
