using Azure;
using Karnel_Travel_Admin_Api.DTO;
using Karnel_Travel_Admin_Api.Interface;
using Karnel_Travel_Admin_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Karnel_Travel_Admin_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{

    private IAccountService accountService;
    private IConfiguration configuration;
    public AccountController(IAccountService _accountService, IConfiguration _configuration)
    {
        accountService = _accountService;
        configuration = _configuration;
        
    }
    [HttpPost("Login")]
    public IActionResult Login(AdminAccount account)
    {
        if (!accountService.Login(account.Username, account.Password))
        {
            return BadRequest(new  {  Msg = "Login failed, please check your information again" });
        }


        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", account.Username),
            new Claim("Role", "Admin")
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: signIn
            );
        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { Code = "200", Msg = "Log in successfully", Token = tokenValue });
    }

}
