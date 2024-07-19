using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IReviewService
{
    public bool create(Review review);
    public bool update(Review review);
    public bool delete(int id);
    public List<ReviewDTO> findAllDTO();
    public ReviewDTO findByIdDTO(int id);
    public Review findById(int id);
    public List<ReviewDTO> findAllDeleted();
    public bool Hide(int id);
    public bool UnHide(int id);
}
