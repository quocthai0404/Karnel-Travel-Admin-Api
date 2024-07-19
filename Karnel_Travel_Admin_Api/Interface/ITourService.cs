using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface ITourService
{
    public List<TourDTOAndMainPhoto> findAllTourDto();

    public List<TourDTOAndMainPhoto> findAllTour();

    public TourDTOAndMainPhoto findById(int id);

    public bool create(Tour tour);
    public bool update(Tour tour);
    public bool delete(int id);
    //public List<> findAllDTO();
    //public  findByIdDTO(int id);
    public Tour findByIdObject(int id);
    public List<TourDTO> findAllDeleted();
    public bool Recover(int id);
}
