namespace Karnel_Travel_Admin_Api.DTO;

public class ReviewDTO
{
    public int ReviewId { get; set; }

    public int ReviewStar { get; set; }

    public string ReviewText { get; set; } = null!;

    public int UserId { get; set; }

    public int? HotelId { get; set; }

    public int? RestaurantId { get; set; }

    public bool IsHide { get; set; }
}
