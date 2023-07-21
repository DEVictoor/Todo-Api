using Microsoft.AspNetCore.Mvc;
using todoapi.models;
using todoapi.Helpers;

namespace todoapi;

public class UserService : IUserService
{
  Context context;
  IPasswordHasher hashingPassword;
  private byte[] salt;

  public UserService(Context ctx, IPasswordHasher hashingPass)
  {
    context = ctx;
    hashingPassword = hashingPass;
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

  public User? FindOne(Guid id)
  {
    var userActual = context.Users.Find(id);
    return userActual;
  }

  public User? FindOneByUsername(string username)
  {
    var foundUser = context.Users.FirstOrDefault<User>(b => b.username == username);
    if (foundUser == null) Console.WriteLine("usuario no encontrado en el servicio");
    return foundUser;
  }

  public async Task Save(User user)
  {
    string salt = hashingPassword.GenerateSalt();
    user.passwordsalt = salt;
    user.password = hashingPassword.GenerateHash(user.password, salt, 10);
    context.Add(user);
    await context.SaveChangesAsync();
  }

  public bool ComparePassword(string pass, User user)
  {
    var hassPass = hashingPassword.GenerateHash(pass, user.passwordsalt, 10);
    var check = hassPass == user.password;
    return check;
  }

  public async Task Update(Guid id, User user)
  {
    var userActual = FindOne(id);
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
  User? FindOneByUsername(string username);
  Task Update(Guid id, User user);
  Task Delete(Guid id);
  bool ComparePassword(string pass, User user);
}
