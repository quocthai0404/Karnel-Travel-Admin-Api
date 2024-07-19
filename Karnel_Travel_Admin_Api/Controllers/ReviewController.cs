using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private IReviewService reviewService;
    public ReviewController(IReviewService _reviewService)
    {
        reviewService = _reviewService;
    }

    [HttpPost("AddReview")]
    public IActionResult AddReview([FromBody] ReviewDTO reviewDTO)
    {
        if (reviewService.create(
            new Models.Review {
                ReviewStar = reviewDTO.ReviewStar,
                ReviewText = reviewDTO.ReviewText,
                UserId = reviewDTO.UserId,
                HotelId = reviewDTO.HotelId,
                RestaurantId = reviewDTO.RestaurantId,
                IsHide = reviewDTO.IsHide,
            }))
        {
            return Ok(new { result = "add review ok" });
        }
        return BadRequest(new { result = "add review failed" });
    }

    [HttpPut("UpdateReview")]
    public IActionResult UpdateReview([FromBody] ReviewDTO reviewDTO)
    {
        var reviewUpdate = reviewService.findById(reviewDTO.ReviewId);
        if (reviewUpdate == null)
        {
            return BadRequest(new { result = "Cannot find review" });
        }
        reviewUpdate.ReviewStar = reviewDTO.ReviewStar;
        reviewUpdate.ReviewText = reviewDTO.ReviewText;
        reviewUpdate.UserId = reviewDTO.UserId;
        reviewUpdate.HotelId = reviewDTO.HotelId;
        reviewUpdate.RestaurantId = reviewDTO.RestaurantId;
        reviewUpdate.IsHide = reviewDTO.IsHide;
        if (reviewService.update(reviewUpdate))
        {
            return Ok(new { result = "update review ok" });
        }
        return BadRequest(new { result = "upload review failed" });
    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (reviewService.delete(id))
        {
            return Ok(new { result = "delete review ok" });
        }
        return BadRequest(new { result = "delete review failed" });
    }

    [HttpGet("findAll")]
    public IActionResult findAll()
    {

        return Ok(reviewService.findAllDTO());
    }

    [HttpGet("findById/{id}")]
    public IActionResult findById(int id)
    {

        return Ok(reviewService.findByIdDTO(id));
    }

    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {

        return Ok(reviewService.findAllDeleted());
    }

    [HttpPost("Hide/{id}")]
    public IActionResult Hide(int id)
    {

        return Ok(reviewService.Hide(id));
    }

    [HttpPost("UnHide/{id}")]
    public IActionResult UnHide(int id)
    {

        return Ok(reviewService.UnHide(id));
    }
}
