using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todoapi.models;

namespace todoapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  IUserService userService;
  IJwtController _jwt;

  public UserController(
      IUserService service,
      IJwtController jwt
  )
  {
    userService = service;
    _jwt = jwt;
  }

  [HttpGet]
  [Authorize]
  public IActionResult Get()
  {
    var identity = HttpContext.User.Identity as ClaimsIdentity;

    var rToken = _jwt.validarToken(identity);

    if (!rToken.success) return Ok(rToken);

    return Ok(userService.Get());
  }

  [HttpPost]
  public IActionResult Save([FromBody] User user)
  {
    var foundUser = userService.FindOneByUsername(user.username);

    if (foundUser != null)
    {
      Console.WriteLine("Usuario encontrado, no se puede encontrar otro");
      return new CustomResponse(HttpStatusCode.Conflict, "Username ya registrado");
    }
    else
    {
      Console.WriteLine("Usuario no encontrado, se creara otro");
      userService.Save(user);
      return new CustomResponse(HttpStatusCode.Accepted, "Usuario guardado");
    }
  }
}
