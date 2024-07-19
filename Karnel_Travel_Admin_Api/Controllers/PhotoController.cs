using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PhotoController : ControllerBase
{
    private IPhotoService photoService;
    public PhotoController(IPhotoService _photoService)
    {
        photoService = _photoService;
    }
    [HttpPost("addPhoto")]
    public IActionResult addPhoto(IFormFile file)
    {

        return Ok(photoService.AddPhoto(file).Url);

    }

    [HttpPost("deletePhoto")]
    public IActionResult deletePhoto(string publicId)
    {

        return Ok(photoService.DeletePhoto(publicId));

    }

    [HttpPost("addListPhoto")]
    public IActionResult addListPhoto(List<IFormFile> files)
    {
        var listPhotoToAdd = new List<Photo>();
        var stringHotelId = Request.Form["hotelId"];
        var stringRoomId = Request.Form["roomId"];
        var stringRestaurantId = Request.Form["restaurantId"];
        var stringSiteId = Request.Form["siteId"];
        var stringBeachId = Request.Form["beachId"];

        //var photoRecord = ;

        var urlList = photoService.AddListPhoto(files);

        foreach (var item in urlList) {
            if (string.IsNullOrEmpty(item))
            {
                return BadRequest(new { msg = "An error occurred while adding photos" });
            }
            else {
                
                listPhotoToAdd.Add(new Photo()
                {
                    HotelId = stringHotelId == "-1" ? null : int.Parse(stringHotelId),
                    RoomId = stringRoomId == "-1" ? null : int.Parse(stringRoomId),
                    RestaurantId = stringRestaurantId == "-1" ? null : int.Parse(stringRestaurantId),
                    BeachId = stringBeachId == "-1" ? null : int.Parse(stringBeachId),
                    SiteId = stringSiteId == "-1" ? null : int.Parse(stringSiteId),
                    PhotoUrl = item
            });
            }
        }
        if (listPhotoToAdd.Count != urlList.Count) {
            return BadRequest(new { msg = "An error has occurred. Please try again later" });
        }
        if (!photoService.addPhotoRecords(listPhotoToAdd)) {
            return BadRequest(new { msg = "An error has occurred. Please try again later" });
        }

        return Ok(new { msg = "All images uploaded successfully" });
        //return Ok(photoService.AddListPhoto(files));

    }
}
