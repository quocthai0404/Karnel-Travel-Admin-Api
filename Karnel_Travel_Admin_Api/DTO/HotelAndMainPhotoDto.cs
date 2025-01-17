﻿namespace Karnel_Travel_Admin_Api.DTO;

public class HotelAndMainPhotoDto
{
	public int HotelId { get; set; }

	public string HotelName { get; set; } = null!;

	public string? HotelDescription { get; set; }

	public string? HotelPriceRange { get; set; }

	public string? HotelLocation { get; set; }

	public int? LocationId { get; set; }

	public bool IsHide { get; set; }

	public List<FacilityDTO> facilities { get; set; }

	public int countReview { get; set; }
	public int totalStar { get; set; }
	public double star { get; set; }

	public string PhotoUrl { get; set; } = "https://res.cloudinary.com/dhee9ysz4/image/upload/v1720448925/dm6mc5s3zagzkl8zrsow.jpg";
}
