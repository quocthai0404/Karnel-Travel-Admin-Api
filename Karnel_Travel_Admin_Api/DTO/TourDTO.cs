namespace Karnel_Travel_Admin_Api.DTO;

public class TourDTO
{
    public int TourId { get; set; }

    public string TourName { get; set; } = null!;

    public string TourDescription { get; set; } = null!;

    public int Departure { get; set; }

    public int Arrival { get; set; }

    public float TourPrice { get; set; }

    public bool IsHide { get; set; }
}
