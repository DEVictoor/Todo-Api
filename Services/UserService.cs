using todoapi.models;

namespace todoapi;

public class UserService : IUserService
{
    Context context;

    public UserService(Context ctx)
    {
        context = ctx;
    }

    public IEnumerable<User> Get()
    {
        return context.Users;
    }

    public async Task Delete(Guid id)
    {
        var userActual = context.Users.Find(id);
        if (userActual != null)
        {
            context.Remove(userActual);
            await context.SaveChangesAsync();
        }
    }

    public async Task Save(User user)
    {
        context.Add(user);
    }

    public async Task Update(Guid id, User user)
    {
        var userActual = context.Users.Find(id);
        if (userActual != null)
        {
            userActual.password = user.password;
            userActual.username = user.username;
            await context.SaveChangesAsync();
        }
    }

}

public interface IUserService
{
    IEnumerable<User> Get();
    Task Save(User user);
    Task Update(Guid id, User user);
    Task Delete(Guid id);
}
