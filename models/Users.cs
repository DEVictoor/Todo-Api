namespace todoapi.models;

public class User
{
  public Guid UserId { get; set; }
  public string username { get; set; }
  public string? passwordsalt { get; set; }
  public string password { get; set; }

  public ICollection<TaskModel>? Tasks { get; }
}
