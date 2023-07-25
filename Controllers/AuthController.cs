using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using todoapi;
using todoapi.models;

namespace todoapi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

  public IUserService _user;
  public IConfiguration _config;
  public AuthController(
  IUserService userService,
  IConfiguration configuration
  )
  {
    _user = userService;
    _config = configuration;
  }

  [HttpPost]
  [Route("login")]
  public dynamic login([FromBody] User user)
  {
    // var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
    User? usuario = _user.FindOneByUsername(user.username);

    if (usuario == null) return new CustomResponse(HttpStatusCode.NotFound, "Usuario no encontrado");

    bool checkPassword = _user.ComparePassword(user.password, usuario);

    if (!checkPassword) return new CustomResponse(HttpStatusCode.Forbidden, "Contraseña incorrecta");

    var jwt = _config.GetSection("Jwt").Get<JwtModel>();

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim("id", usuario.UserId.ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      jwt.Issuer,
      jwt.Audience,
      claims,
      expires: DateTime.Now.AddMinutes(60),
      signingCredentials: signIn
    );

    return new
    {
      success = true,
      message = "exito",
      token = new JwtSecurityTokenHandler().WriteToken(token)
    };
  }

  [HttpPost]
  [Route("register")]
  public dynamic register([FromBody] RegisterModel model)
  {
    try
    {
      _user.Save(model);
      return new
      {
        success = true,
        message = "Registro con exito",
      };
    }
    catch (System.Exception)
    {
      return new
      {
        success = false,
        message = "No se pudo registrar el usuario",
      };
      // throw;
    }
  }
}
