using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private IHotelService hotelService;
    public HotelController(IHotelService _hotelService)
    {
        hotelService = _hotelService;
    }

    [HttpPost("AddHotel")]
    public IActionResult AddHotel([FromBody] HotelDTO hotelDto)
    {
        if (hotelService.create(new Models.Hotel
        {
            HotelName = hotelDto.HotelName,
            HotelDescription = hotelDto.HotelDescription,
            HotelPriceRange = hotelDto.HotelPriceRange,
            HotelLocation = hotelDto.HotelLocation,
            LocationId = hotelDto.LocationId
        }))
        {
            return Ok(new { result = "add hotel ok" });
        }
        return BadRequest(new { result = "add hotel failed" });
    }

    [HttpPut("UpdateHotel")]
    public IActionResult UpdateHotel([FromBody] HotelDTO hotelDto)
    {
        var hotel = hotelService.findById(hotelDto.HotelId);
        if (hotel == null)
        {
            return BadRequest(new { result = "Cannot find hotel" });
        }

        hotel.HotelName = hotelDto.HotelName;
        hotel.HotelDescription = hotelDto.HotelDescription;
        hotel.HotelPriceRange = hotelDto.HotelPriceRange;
        hotel.HotelLocation = hotelDto.HotelLocation;
        hotel.LocationId = hotelDto.LocationId;

        if (hotelService.update(hotel))
        {
            return Ok(new { result = "update hotel ok" });
        }
        return BadRequest(new { result = "upload hotel failed" });
    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (hotelService.delete(id))
        {
            return Ok(new { result = "delete hotel ok" });
        }
        return BadRequest(new { result = "delete hotel failed" });
    }

    [HttpGet("findAll")]
    public IActionResult findAll()
    {

        return Ok(hotelService.listHotel());
    }

    [HttpGet("findById/{id}")]
    public IActionResult findById(int id)
    {

        return Ok(hotelService.findByIdDTO(id));
    }

    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {

        return Ok(hotelService.findAllDeleted());
    }

    [HttpPost("recover/{id}")]
    public IActionResult recover(int id)
    {

        return Ok(hotelService.Recover(id));
    }
}
