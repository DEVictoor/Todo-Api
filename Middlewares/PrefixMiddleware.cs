namespace todoapi.Middlewares;

public class PrefixMiddleware
{
  private readonly RequestDelegate _next;
  private readonly string _routerPrefix;

  public PrefixMiddleware(RequestDelegate next, string prefix)
  {
    _next = next;
    _routerPrefix = prefix;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    context.Request.PathBase = new PathString(_routerPrefix);
    await _next(context);
  }
}