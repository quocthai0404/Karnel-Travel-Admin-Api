using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AirportController : ControllerBase
{
    private IAirportService airportService;
    public AirportController(IAirportService _airportService)
    {
        airportService = _airportService;
    }
    [HttpPost("addAirport")]
    public IActionResult addAirport([FromBody] AirportDTO ap)
    {
        if (airportService.create(new Models.Airport { AirportId = ap.AirportId, AirportName = ap.AirportName }))
        {
            return Ok(new { result = "add airport ok" });
        }
        return BadRequest(new { result = "add airport failed" });
    }

    [HttpPut("updateAirport")]
    public IActionResult updateAirport([FromBody] AirportDTO ap)
    {
        var airport = airportService.findById(ap.AirportId);
        if (airport == null) {
            return BadRequest(new { result = "cannot find airpod" });
        }
        airport.AirportName = ap.AirportName;
        if (airportService.update(airport))
        {
            return Ok(new { result = "update airport ok" });
        }
        return BadRequest(new { result = "update airport failed" });
    }

    [HttpGet("getAirportById/{id}")]
    public IActionResult getById(string id)
    {
        return Ok(airportService.findByIdDTO(id));
    }

    [HttpPost("deleteById/{id}")]
    public IActionResult Delete(string id)
    {
        return Ok(new { result = airportService.delete(id) });
    }

    [HttpGet("findall")]
    public IActionResult findall()
    {
        return Ok(new { result = airportService.findAll() });
    }

    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {
        return Ok(new { result = airportService.findAllDeleted() });
    }

    [HttpPost("Recover/{airportId}")]
    public IActionResult Recover(string airportId)
    {
        return Ok(new { result = airportService.Recover(airportId) });
    }
}
