using Karnel_Travel_Admin_Api.Interface;
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

        return Ok(photoService.AddListPhoto(files));

    }
}
