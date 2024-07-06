using Karnel_Travel_Admin_Api.Converters;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private IFlightService flightService;
    public FlightController(IFlightService _flightService)
    {
        flightService = _flightService;
    }

    [HttpPost("AddFlight")]
    public IActionResult AddFlight([FromBody] FlightDTO flight)
    {
        if (flightService.create(new Models.Flight
        {
            DepartureAirportId = flight.DepartureAirportId,
            ArrivalAirportId = flight.ArrivalAirportId,
            StartDate = flight.StartDate,
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            FlightPrice = flight.FlightPrice,
        }))
        {
            return Ok(new { result = "add flight ok" });
        }
        return BadRequest(new { result = "add flight failed" });
       
    }

    [HttpGet("findById/{id}")]
    public IActionResult findById(int id)
    {
        return Ok(flightService.findByIdDTO(id));
        
    }

    [HttpPost("delete/{id}")]
    public IActionResult deleteById(int id) {
        return Ok(flightService.delete(id));
    }

    [HttpPut("UpdateFlight")]
    public IActionResult UpdateFlight([FromBody] FlightDTO flight)
    {
        if (flightService.update(new Models.Flight
        {
            FlightId = flight.FlightId,
            DepartureAirportId = flight.DepartureAirportId,
            ArrivalAirportId = flight.ArrivalAirportId,
            StartDate = flight.StartDate,
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            FlightPrice = flight.FlightPrice
        }))
        {
            return Ok(new { result = "update flight ok" });
        }
        return BadRequest(new { result = "update flight failed" });
    }

    [HttpGet("findAllDeleted")]
    public IActionResult UpdatefindAllDeletedFlight()
    {
        return Ok(flightService.findAllDeleted());
    }

    [HttpGet("findAll")]
    public IActionResult findAll()
    {
        return Ok(flightService.findAllDTO());
    }
    [HttpPost("Recover/{id}")]
    public IActionResult Recover(int id)
    {
        return Ok(flightService.Recover(id));
    }
}
