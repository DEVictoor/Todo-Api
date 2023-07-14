using Microsoft.AspNetCore.Mvc;

namespace todoapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    IUserService userService;

    // private readonly I

    public UserController(
        IUserService service
    )
    {
        userService = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(userService.Get());
    }
}
