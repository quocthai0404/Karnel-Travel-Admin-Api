using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FacilityController : ControllerBase
{
    private IFacilityService facilityService;
    public FacilityController(IFacilityService facilityService)
    {
        this.facilityService = facilityService;
    }
    [HttpGet("findAll")]
    public IActionResult findAll() {
        return Ok(facilityService.findAll());
    }

    [HttpPost("addFacility")]
    public IActionResult addFacility([FromBody] FacilityDTO facilityDTO)
    {
        if (facilityService.create(new Models.Facility { FacilityName = facilityDTO.FacilityName }))
        {
            return Ok(new { result = "add facility ok" });
        }
        return BadRequest(new { result = "add facility failed" });
    }

    [HttpPut("UpdateFacility")]
    public IActionResult UpdateFacility([FromBody] FacilityDTO facilityDTO)
    {
        var facility = facilityService.findById(facilityDTO.FacilityId);
        if (facility==null) { 
            return BadRequest(new { result = "Cannot Find Facility" });
        }
        facility.FacilityName = facilityDTO.FacilityName;
        if (facilityService.update(facility))
        {
            return Ok(new { result = "Update Facility ok" });
        }
        return BadRequest(new { result = "Upload Facility failed" });
    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (facilityService.delete(id))
        {
            return Ok(new { result = "delete facility ok" });
        }
        return BadRequest(new { result = "delete facility failed" });
    }

    [HttpGet("findById/{id}")]
    public IActionResult findById(int id)
    {

        return Ok(facilityService.findByIdDTO(id));
    }
    
    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {

        return Ok(facilityService.findAllDeleted());
    }

    [HttpPost("recover/{id}")]
    public IActionResult recover(int id)
    {

        return Ok(facilityService.Recover(id));
    }
}
