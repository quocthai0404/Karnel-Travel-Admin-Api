namespace Karnel_Travel_Admin_Api.DTO;

public class LocationDTO
{
    public int LocationId { get; set; }

    public string? LocationNumber { get; set; }

    public int ProvinceId { get; set; }

    public int DistrictId { get; set; }

    public int WardId { get; set; }

    public int StreetId { get; set; }

    public bool IsHide { get; set; }
}
