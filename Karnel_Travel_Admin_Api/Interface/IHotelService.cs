using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IHotelService
{
    public bool create(Hotel hotel);
    public bool update(Hotel hotel);
    public bool delete(int id);
    public Hotel findById(int id);
    public List<HotelDTO> findAllDTO();
    public HotelDTO findByIdDTO(int id);
    public List<HotelDTO> findAllDeleted();
    public bool Recover(int id);
}
