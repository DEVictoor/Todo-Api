using todoapi;
using todoapi.models;

namespace todoapi.services;

public class TaskService : ITaskService
{
  Context context;
  IUserService _user;
  public TaskService(Context ctx, IUserService sUser)
  {
    context = ctx;
    _user = sUser;
  }

  public IEnumerable<TaskModel> Get()
  {
    return context.Tasks;
  }

  public async Task Delete(Guid id)
  {
    var taskActual = context.Tasks.Find(id);
    if (taskActual == null) throw new Exception("No existe una tarea con ese id");
    context.Remove(taskActual);
    await context.SaveChangesAsync();
  }

  public TaskModel? FindOne(Guid id)
  {
    var taskActual = context.Tasks.Find(id);
    return taskActual;
  }

  public async Task Save(TaskModel model)
  {
    var userFound = _user.FindOne(model.UserId);
    if (userFound == null) throw new Exception("Envie un id para el usuario valido");
    context.Add(model);
    await context.SaveChangesAsync();
  }

  public async Task Update(Guid id, TaskModel mTask)
  {
    var TaskActual = FindOne(id);
    if (TaskActual == null) throw new Exception("Envie un id para el tarea valido");

    var userFound = _user.FindOne(mTask.UserId);
    if (userFound == null) throw new Exception("Envie un id para el usuario valido");

    TaskActual.description = mTask.description;
    TaskActual.UserId = mTask.UserId;
    TaskActual.IsDone = mTask.IsDone;

    await context.SaveChangesAsync();
  }
}

public interface ITaskService
{
  IEnumerable<TaskModel> Get();
  Task Save(TaskModel m);
  TaskModel? FindOne(Guid id);
  Task Update(Guid id, TaskModel m);
  Task Delete(Guid id);
}