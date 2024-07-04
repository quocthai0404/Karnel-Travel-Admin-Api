namespace Karnel_Travel_Admin_Api.DTO;

public class testDTO
{
    public int FlightId { get; set; }

    public string DepartureAirportId { get; set; } = null!;

    public string ArrivalAirportId { get; set; } = null!;

    public string StartDate { get; set; }

    public string DepartureTime { get; set; }

    public string ArrivalTime { get; set; }

    public float FlightPrice { get; set; }

    public bool IsHide { get; set; }
}
