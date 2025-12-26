using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShelfTagsBE.Dto;
using ShelfTagsBE.Models;
using ShelfTagsBE.Service;

namespace ShelfTagsBE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AccountService accountService, JwtService jwtService) : ControllerBase
    {
        
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] AccountDTO dto)
{
    var account = await accountService.Login(dto.Username, dto.Password);
    if (account == null)
        return Unauthorized();

    var token = jwtService.GenerateToken();
    return Ok(token);
}
    }
}
