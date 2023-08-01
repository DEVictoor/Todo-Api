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
    modelBuilder.Entity<User>(user =>
    {
      user.ToTable("User");
      user.HasKey(p => p.UserId);

      user.HasIndex(p => p.username).IsUnique();
      user.Property(p => p.password).IsRequired();
      user.Property(p => p.passwordsalt).HasColumnType("varchar(200)");
      user.HasMany(p => p.Tasks).WithOne(p => p.User).HasForeignKey(p => p.UserId).HasPrincipalKey(p => p.UserId);
    });

    // TODO: Model Task
    modelBuilder.Entity<TaskModel>(task =>
    {
      task.ToTable("Task");
      task.HasKey(p => p.TaskId);
      task.Property(p => p.description).IsRequired();
      task.Property(p => p.IsDone).IsRequired();
      task.Property(p => p.Created_at).HasDefaultValue(DateTime.UtcNow);
    });
  }
}
