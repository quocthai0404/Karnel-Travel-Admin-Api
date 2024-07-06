using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private ILocationService locationService;
    public LocationController(ILocationService _locationService)
    {
        locationService = _locationService;
    }

    [HttpGet("findAllProvince")]
    public IActionResult findAllProvince()
    {
        return Ok(locationService.findAllProvinceDTO());
    }

    [HttpGet("findAllDistrict/{ProvinceId}")]
    public IActionResult findAllProvince(int ProvinceId)
    {
        return Ok(locationService.findAllDistrictDTO(ProvinceId));
    }

    [HttpGet("findAllWard/{DistrictId}")]
    public IActionResult findAllWard(int DistrictId)
    {
        return Ok(locationService.findAllWardDTO(DistrictId));
    }

    [HttpGet("findAllStreet/{WardId}")]
    public IActionResult findAllStreet(int WardId)
    {
        return Ok(locationService.findAllStreetDTO(WardId));
    }

    [HttpPost("addStreet")]
    public IActionResult addStreet([FromBody] StreetDTO streetDTO)
    {
        if (locationService.AddStreet(new Models.Street { StreetName = streetDTO.StreetName, WardId = streetDTO.WardId }))
        {
            return Ok(new { Result = "Add Street OK" });
        }
        return BadRequest(new { Result = "Add Street Failed" });
    }

    [HttpPost("addLocation")]
    public IActionResult addLocation([FromBody] LocationDTO locationDTO)
    {
        if (locationService.AddLocation(new Location
        {
            LocationNumber = locationDTO.LocationNumber,
            ProvinceId = locationDTO.ProvinceId,
            DistrictId = locationDTO.DistrictId,
            WardId = locationDTO.WardId,
            StreetId = locationDTO.StreetId
        }))
        {
            return Ok(new { Result = "Add Location OK" });
        }
        return BadRequest(new { Result = "Add Location Failed" });
    }

    [HttpGet("findByIdLocation/{id}")]
    public IActionResult findByIdLocation(int id)
    {

        return Ok(locationService.findByIdLocation(id));

    }

    [HttpGet("findByIdLocationDTO/{id}")]
    public IActionResult findByIdLocationDTO(int id)
    {

        return Ok(locationService.findByIdDTO(id));

    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (locationService.DeleteLocation(id))
        {
            return Ok(new { result = "delete location ok" });
        }
        return BadRequest(new { result = "delete location failed" });
    }

    [HttpPost("recover/{id}")]
    public IActionResult recover(int id)
    {
        return Ok(locationService.RecoverLocation(id));
    }

    [HttpPut("UpdateStreet")]
    public IActionResult UpdateStreet([FromBody] StreetDTO streetDTO)
    {
        var streetUpdate = locationService.findStreetById(streetDTO.StreetId);
        if (streetUpdate == null)
        {
            return BadRequest(new { result = "Cannot find street" });
        }
        streetUpdate.StreetName = streetDTO.StreetName;
        streetUpdate.WardId = streetDTO.WardId;
        if (locationService.update(streetUpdate))
        {
            return Ok(new { result = "update street ok" });
        }
        return BadRequest(new { result = "upload street failed" });
    }

    [HttpPut("UpdateLocation")]
    public IActionResult UpdateLocation([FromBody] LocationDTO locationDTO)
    {
        var location = locationService.findById(locationDTO.LocationId);
        if (location == null)
        {
            return BadRequest(new { result = "Cannot find location" });
        }
        location.LocationNumber = locationDTO.LocationNumber;
        location.ProvinceId = locationDTO.ProvinceId;
        location.DistrictId = locationDTO.DistrictId;
        location.WardId = locationDTO.WardId;
        location.StreetId = locationDTO.StreetId;
        if (locationService.update(location))
        {
            return Ok(new { result = "update location ok" });
        }
        return BadRequest(new { result = "upload location failed" });
    }
}
