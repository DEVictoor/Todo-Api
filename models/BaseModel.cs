namespace todoapi.models;

public abstract class BaseModel
{
  public DateTime? Created_at { get; set; } = DateTime.UtcNow;
  // public DateTime? Updated_at { get; set; }
}