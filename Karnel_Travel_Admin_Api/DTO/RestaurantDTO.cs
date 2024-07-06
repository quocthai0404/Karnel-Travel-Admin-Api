namespace Karnel_Travel_Admin_Api.DTO;

public class RestaurantDTO
{
    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; } = null!;

    public string RestaurantDescription { get; set; } = null!;

    public string RestaurantPriceRange { get; set; } = null!;

    public string? RestaurantLocation { get; set; }

    public int? LocationId { get; set; }

    public bool IsHide { get; set; }
}
