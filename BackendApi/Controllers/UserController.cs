using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/users/{userId}")]
public class UserController : ControllerBase
{
    public UserController()
    {
        
    }
    
    [HttpGet("subscriptions",Name = "GetUserSubscriptions")]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}