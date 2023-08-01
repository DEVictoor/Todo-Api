using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todoapi.services;
using todoapi.models;

namespace todoapi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class TaskController : ControllerBase
{
  ITaskService _task;
  IJwtController _jwt;

  public TaskController(
    ITaskService service,
    IJwtController jwt
  )
  {
    _task = service;
    _jwt = jwt;
  }

  [HttpGet]
  public IActionResult Get()
  {
    return Ok(_task.Get());
  }

  [HttpGet]
  [Route("{id:guid}")]
  public IActionResult FindOne(Guid id)
  {
    return Ok(_task.FindOne(id));
  }

  [HttpPost]
  public async Task<IActionResult> Save([FromBody] TaskModel model)
  {
    var identity = HttpContext.User.Identity as ClaimsIdentity;
    var rToken = _jwt.validarToken(identity);
    if (!rToken.success) return Ok(rToken);
    // return rToken.result.UserId;
    var id = rToken.result.UserId;
    Console.WriteLine(id);
    // var id = GetUserId();
    model.UserId = id;
    await _task.Save(model);
    return Ok();
  }

  [HttpDelete]
  [Route("{id:guid}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    await _task.Delete(id);
    return Ok();
  }

  [HttpPut]
  [Route("{id:guid}")]
  public IActionResult Update(Guid id, [FromBody] TaskModel model)
  {
    return Ok(_task.Update(id, model));
  }

  // public Guid GetUserId()
  // {
  //   var identity = HttpContext.User.Identity as ClaimsIdentity;
  //   var rToken = _jwt.validarToken(identity);
  //   if (!rToken.success) return Ok(rToken);
  //   return rToken.result.UserId;
  // }
}