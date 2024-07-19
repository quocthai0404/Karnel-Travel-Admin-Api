namespace Karnel_Travel_Admin_Api.DTO;

public class RoomDTO
{
    public int RoomId { get; set; }

    public int HotelId { get; set; }

    public string RoomName { get; set; } = null!;

    public string RoomDescription { get; set; } = null!;

    public float RoomPrice { get; set; }

    public int NumOfSingleBed { get; set; }

    public int NumOfDoubleBed { get; set; }

    public bool IsHide { get; set; }

}
