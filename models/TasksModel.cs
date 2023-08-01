namespace todoapi.models;

public class TaskModel : BaseModel
{
  public Guid TaskId { get; set; }
  public string? description { get; set; }
  public Boolean IsDone { get; set; } = false;
  public User? User { get; set; }
  public Guid UserId { get; set; }
}
