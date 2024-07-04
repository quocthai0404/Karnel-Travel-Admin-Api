using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IBeachService
{
    public bool create(Beach beach);
    public bool update(Beach beach);
    public bool delete(int id);
    public List<BeachDTO> findAllDTO();
    public BeachDTO findByIdDTO(int id);

    public List<BeachDTO> findAllDeleted();
    public bool Recover(int id);
}
