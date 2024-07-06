using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private IDiscountService discountService;
    public DiscountController(IDiscountService _discountService)
    {
        discountService = _discountService;
    }

    [HttpGet("findAll")]
    public IActionResult findAll() {
        return Ok(discountService.findAll());
    }

    [HttpPost("addDiscount")]
    public IActionResult addDiscount([FromBody] Discount discount)
    {
        if (discountService.create(discount)) {
            return Ok(new { result = "Add Discount Ok" });
        }
        return BadRequest(new { result = "Add Discount Failed" });
    }

    [HttpPut("updateDiscount")]
    public IActionResult updateDiscount([FromBody] Discount discount)
    {
        if (discountService.update(discount))
        {
            return Ok(new { result = "Update Discount Ok" });

        }
        return BadRequest(new { result = "Update Discount Failed" });
    }
}
