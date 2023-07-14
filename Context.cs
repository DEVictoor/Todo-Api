using Microsoft.EntityFrameworkCore;
using todoapi.models;

namespace todoapi;

public class Context : DbContext
{
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    // Construct
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // List<User> UserInit  = new List<User>();
        // UserInit.Add(new User() {UserId = Guid.Parse("")})
        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("User");
            user.HasKey(p => p.UserId);

            user.HasIndex(p => p.username).IsUnique();
            user.Property(p => p.password).IsRequired();
        });

        // TODO: Model Task
        modelBuilder.Entity<TaskModel>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);
        });
    }
}
