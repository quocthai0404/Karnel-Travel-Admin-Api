using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface ILocationService
{
    public List<ProvinceDTO> findAllProvinceDTO();
    public List<DistrictDTO> findAllDistrictDTO(int provinceId);
    public List<WardDTO> findAllWardDTO(int districtId);
    public List<StreetDTO> findAllStreetDTO(int wardId);
    public bool AddStreet(Street street);
    public Street findStreetById(int id);
    //public bool UpdateStreet(Street street);
    public Location findById(int id);
    public LocationDTO findByIdDTO(int id);
    public StringLocationDTO findByIdLocation(int id);
    public bool AddLocation(Location location);
    public bool DeleteLocation(int id);
    public bool RecoverLocation(int id);
    //public bool UpdateLocation(Location location);
    public bool update<T>(T entity);

}
