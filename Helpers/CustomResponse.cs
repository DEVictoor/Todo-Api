using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace todoapi;

public class CustomResponse : IActionResult
{
  private readonly HttpStatusCode _status;
  private readonly string _responseMessage;

  public CustomResponse(HttpStatusCode status, string responseMessage)
  {
    _status = status;
    _responseMessage = responseMessage;
  }

  public async Task ExecuteResultAsync(ActionContext context)
  {
    var objectResult = new ObjectResult(new
    {
      responseMessage = _responseMessage
    })
    {
      StatusCode = (int)_status,
    };
    context.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = _responseMessage;
    await objectResult.ExecuteResultAsync(context);
  }
}