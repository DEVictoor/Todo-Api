using System.Security.Claims;
using todoapi.models;
namespace todoapi.Controllers;

public class JwtController : IJwtController
{
  public Context context;
  public JwtController(Context ctx)
  {
    context = ctx;
  }

  public dynamic validarToken(ClaimsIdentity identity)
  {
    // var idtest = identity.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
    // Console.WriteLine(idtest);
    try
    {
      if (identity.Claims.Count() == 0)
      {
        return new
        {
          success = false,
          message = "Verificar si estas enviando un token",
          result = ""
        };
      }

      var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

      User usuario = context.Users.FirstOrDefault(x => x.UserId.ToString() == id);

      return new
      {
        success = true,
        message = "exito",
        result = usuario
      };
    }
    catch (Exception e)
    {
      return new
      {
        success = false,
        message = "Catch: " + e.Message,
      };
    }

  }
}

public interface IJwtController
{
  dynamic validarToken(ClaimsIdentity identity);
}