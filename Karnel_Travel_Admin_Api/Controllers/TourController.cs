using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TourController : ControllerBase
{
    private ITourService tourService;
    public TourController(ITourService _tourService)
    {
        tourService = _tourService;
    }
    [HttpGet("getAllTour")]
    public IActionResult getAllTour()
    {

        return Ok(tourService.findAllTour());

    }

    [HttpGet("getTourDetail/{id}")]
    public IActionResult getAllTour(int id)
    {

        return Ok(tourService.findById(id));

    }

    [HttpPost("AddTour")]
    public IActionResult AddBeach([FromBody] TourDTO tourDTO)
    {
        if (tourService.create(new Models.Tour {
           TourName = tourDTO.TourName,
            TourDescription = tourDTO.TourDescription,
            Departure = tourDTO.Departure,
            Arrival = tourDTO.Arrival, 
            TourPrice = tourDTO.TourPrice
            
        }))
        {
            return Ok(new { result = "add tour ok" });
        }
        return BadRequest(new { result = "add tour failed" });
    }

    [HttpPut("UpdateTour")]
    public IActionResult UpdateTour([FromBody] TourDTO tourDto)
    {
        var tourUpdate = tourService.findByIdObject(tourDto.TourId);
        if (tourUpdate == null)
        {
            return BadRequest(new { result = "Cannot find tour" });
        }
        tourUpdate.TourName = tourDto.TourName;
        tourUpdate.TourDescription = tourDto.TourDescription;
        tourUpdate.Departure = tourDto.Departure;
        tourUpdate.Arrival = tourDto.Arrival;
        tourUpdate.TourPrice = tourDto.TourPrice;
        if (tourService.update(tourUpdate))
        {
            return Ok(new { result = "update tour ok" });
        }
        return BadRequest(new { result = "upload tour failed" });
    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (tourService.delete(id))
        {
            return Ok(new { result = "delete tour ok" });
        }
        return BadRequest(new { result = "delete tour failed" });
    }

    //[HttpGet("findById/{id}")]
    //public IActionResult findById(int id)
    //{

    //    return Ok(tourService.findById(id));
    //}

    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {

        return Ok(tourService.findAllDeleted());
    }

    [HttpPost("recover/{id}")]
    public IActionResult recover(int id)
    {

        return Ok(tourService.Recover(id));
    }
}
