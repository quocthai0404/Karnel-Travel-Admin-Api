using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IRestaurantService
{
    public bool create(Restaurant restaurant);
    public bool update(Restaurant restaurant);
    public bool delete(int id);

    public RestaurantDTO findByIdDTO(int id);
    public Restaurant findById(int id);
    
    public List<RestaurantDTO> findAllDTO();
    public List<RestaurantDTO> findAllDeleted();
    public bool Recover(int id);
}
