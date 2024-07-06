using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private IRestaurantService restaurantService;
    public RestaurantController(IRestaurantService _restaurantService)
    {
        restaurantService = _restaurantService;
    }

    [HttpPost("AddRestaurant")]
    public IActionResult AddRestaurant([FromBody] RestaurantDTO restaurantDTO)
    {
        if (restaurantService.create(new Models.Restaurant {
            RestaurantName = restaurantDTO.RestaurantName,
            RestaurantDescription = restaurantDTO.RestaurantDescription,
            RestaurantPriceRange = restaurantDTO.RestaurantPriceRange,
            RestaurantLocation = restaurantDTO.RestaurantLocation,
            LocationId = restaurantDTO.LocationId
        }))
        {
            return Ok(new { result = "add restaurant ok" });
        }
        return BadRequest(new { result = "add restaurant failed" });
    }

    [HttpPut("UpdateRestaurant")]
    public IActionResult UpdateBeach([FromBody] RestaurantDTO restaurantDTO)
    {
        var restaurantUpdate = restaurantService.findById(restaurantDTO.RestaurantId);
        if (restaurantUpdate == null)
        {
            return BadRequest(new { result = "Cannot find restaurant" });
        }
        restaurantUpdate.RestaurantName = restaurantDTO.RestaurantName;
        restaurantUpdate.RestaurantDescription = restaurantDTO.RestaurantDescription;
        restaurantUpdate.RestaurantPriceRange = restaurantDTO.RestaurantPriceRange;
        restaurantUpdate.RestaurantLocation = restaurantDTO.RestaurantLocation;
        restaurantUpdate.LocationId = restaurantDTO.LocationId;
        if (restaurantService.update(restaurantUpdate))
        {
            return Ok(new { result = "update restaurant ok" });
        }
        return BadRequest(new { result = "upload restaurant failed" });
    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (restaurantService.delete(id))
        {
            return Ok(new { result = "delete restaurant ok" });
        }
        return BadRequest(new { result = "delete restaurant failed" });
    }

    [HttpGet("findAll")]
    public IActionResult findAll()
    {

        return Ok(restaurantService.findAllDTO());
    }

    [HttpGet("findById/{id}")]
    public IActionResult findById(int id)
    {

        return Ok(restaurantService.findByIdDTO(id));
    }

    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {

        return Ok(restaurantService.findAllDeleted());
    }

    [HttpPost("recover/{id}")]
    public IActionResult recover(int id)
    {

        return Ok(restaurantService.Recover(id));
    }
}
