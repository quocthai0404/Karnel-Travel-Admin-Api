using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private IRoomService roomService;
    public RoomController(IRoomService _roomService)
    {
        roomService = _roomService;
    }

    [HttpPost("AddRoom")]
    public IActionResult AddRoom([FromBody] RoomDTO roomDTO)
    {
        if (roomService.create(new Models.Room {
            HotelId = roomDTO.HotelId,
            RoomName = roomDTO.RoomName,
            RoomDescription =  roomDTO.RoomDescription,
            RoomPrice = roomDTO.RoomPrice,
            NumOfSingleBed = roomDTO.NumOfSingleBed,
            NumOfDoubleBed = roomDTO.NumOfDoubleBed
        }))
        {
            return Ok(new { result = "add room ok" });
        }
        return BadRequest(new { result = "add room failed" });
    }
    [HttpPut("UpdateRoom")]
    public IActionResult UpdateRoom([FromBody] RoomDTO roomDto)
    {
        var roomUpdate = roomService.findById(roomDto.RoomId);
        if (roomUpdate == null)
        {
            return BadRequest(new { result = "Cannot find room" });
        }
        roomUpdate.HotelId = roomDto.HotelId;
        roomUpdate.RoomName = roomDto.RoomName;
        roomUpdate.RoomDescription = roomDto.RoomDescription;
        roomUpdate.RoomPrice = roomDto.RoomPrice;
        roomUpdate.NumOfSingleBed = roomDto.NumOfSingleBed;
        roomUpdate.NumOfDoubleBed = roomDto.NumOfDoubleBed;
        if (roomService.update(roomUpdate))
        {
            return Ok(new { result = "update room ok" });
        }
        return BadRequest(new { result = "upload room failed" });
    }

    [HttpPost("delete/{id}")]
    public IActionResult delete(int id)
    {
        if (roomService.delete(id))
        {
            return Ok(new { result = "delete room ok" });
        }
        return BadRequest(new { result = "delete room failed" });
    }

    [HttpGet("findAll")]
    public IActionResult findAll()
    {

        return Ok(roomService.findAll());
    }

    [HttpGet("findById/{id}")]
    public IActionResult findById(int id)
    {

        return Ok(roomService.findByIdDTO(id));
    }
    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {

        return Ok(roomService.findAllDeleted());
    }

    [HttpPost("recover/{id}")]
    public IActionResult recover(int id)
    {

        return Ok(roomService.Recover(id));
    }
}
