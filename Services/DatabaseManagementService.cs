using Microsoft.EntityFrameworkCore;

namespace todoapi;
public static class DatabaseManagementService
{
  public static void MigrationInitialisation(IApplicationBuilder app)
  {
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
      serviceScope.ServiceProvider.GetService<Context>().Database.Migrate();
    }
  }
}