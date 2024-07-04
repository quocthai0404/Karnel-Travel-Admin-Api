using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IFlightService
{
    public bool create(Flight flight);
    public bool update(Flight flight);
    public bool delete(int id);
    public List<FlightDTO> findAllDTO();
    public FlightDTO findByIdDTO(int id);

    public List<FlightDTO> findAllDeleted();
    public bool Recover(int id);
}
