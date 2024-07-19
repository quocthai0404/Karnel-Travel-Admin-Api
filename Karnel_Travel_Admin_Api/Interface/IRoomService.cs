using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IRoomService
{
    public bool create(Room room);
    public bool update(Room room);
    public bool delete(int roomId);
    public Room findById(int roomId);
    public RoomDTO findByIdDTO(int roomId);
    public List<RoomDTO> findAll();
    public List<RoomDTO> findAllDeleted();
    public bool Recover(int roomId);
}
