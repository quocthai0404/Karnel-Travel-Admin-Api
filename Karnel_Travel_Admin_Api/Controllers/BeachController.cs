using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BeachController : ControllerBase
{
    private IBeachService beachService;
    public BeachController(IBeachService _beachService)
    {
        beachService = _beachService;
    }

    [HttpPost("AddBeach")]
    public IActionResult AddBeach([FromBody] BeachDTO beach) {
        if (beachService.create(new Models.Beach { BeachName = beach.BeachName, BeachLocation = beach.BeachLocation, LocationId = beach.LocationId }))
        {
            return Ok(new { result = "add beach ok" });
        }
        return BadRequest(new { result = "add beach failed" });
    }

    [HttpPut("UpdateBeach")]
    public IActionResult UpdateBeach([FromBody] BeachDTO beach)
    {
        var beachUpdate = beachService.findById(beach.BeachId);
        if (beachUpdate == null) {
            return BadRequest(new { result = "Cannot find beach" });
        }
        beachUpdate.BeachName = beach.BeachName;
        beachUpdate.BeachLocation = beach.BeachLocation;
        beachUpdate.LocationId = beach.LocationId;
        if (beachService.update(beachUpdate))
        {
            return Ok(new { result = "update beach ok" });
        }
        return BadRequest(new { result = "upload beach failed" });
    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (beachService.delete(id))
        {
            return Ok(new { result = "delete beach ok" });
        }
        return BadRequest(new { result = "delete beach failed" });
    }

    [HttpGet("findAll")]
    public IActionResult findAll()
    {
        
        return Ok(beachService.findAllDTO());
    }

    [HttpGet("findById/{id}")]
    public IActionResult findById(int id)
    {

        return Ok(beachService.findByIdDTO(id));
    }

    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {

        return Ok(beachService.findAllDeleted());
    }

    [HttpPost("recover/{id}")]
    public IActionResult recover(int id)
    {

        return Ok(beachService.Recover(id));
    }
}
