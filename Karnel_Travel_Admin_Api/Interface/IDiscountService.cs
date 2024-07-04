using Karnel_Travel_Admin_Api.Models;

namespace Karnel_Travel_Admin_Api.Interface;

public interface IDiscountService
{
    public bool create(Discount discount);
    public bool update(Discount discount);

    public List<Discount> findAll();

}
