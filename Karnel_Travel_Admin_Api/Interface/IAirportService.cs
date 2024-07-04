using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IAirportService
{
    public bool create(Airport airport);
    public bool update(Airport airport);
    public bool delete(string airportId);
    public Airport findById(string airportId);
    public AirportDTO findByIdDTO(string airportId);
    public List<AirportDTO> findAll();
    public List<AirportDTO> findAllDeleted();
    public bool Recover(string airportId);
}
