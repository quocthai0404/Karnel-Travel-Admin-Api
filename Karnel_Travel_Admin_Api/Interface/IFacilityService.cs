using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IFacilityService
{
    public bool create(Facility facility);
    public bool update(Facility facility);
    public bool delete(int id);
    public FacilityDTO findByIdDTO(int id);
    public Facility findById(int id);
    public List<FacilityDTO> findAll();
    public List<FacilityDTO> findAllDeleted();
    public bool Recover(int id);
}
