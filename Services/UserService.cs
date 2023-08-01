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

  public async Task Save(RegisterModel mRegister)
  {
    User createuser = new User();
    string salt = hashingPassword.GenerateSalt();
    createuser.username = mRegister.username;
    createuser.passwordsalt = salt;
    createuser.password = hashingPassword.GenerateHash(mRegister.password, salt, 10);
    context.Add(createuser);
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
  Task Save(RegisterModel u);
  User? FindOneByUsername(string username);
  User? FindOne(Guid id);
  Task Update(Guid id, User user);
  Task Delete(Guid id);
  bool ComparePassword(string pass, User user);
}
