using System.Security.Cryptography;
using System.Text;

namespace todoapi.Helpers;

public class PasswordHasher : IPasswordHasher
{
  public string _pepper;

  public PasswordHasher()
  {
    _pepper = Environment.GetEnvironmentVariable("PEPPER") ?? "changeme";
  }

  public string GenerateHash(string password, string salt, int iterator = 9)
  {
    if (iterator <= 0) return password;

    using var sha256 = SHA256.Create();
    var passwordSaltPepper = $"{password}{salt}{_pepper}";
    var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
    var byteHash = sha256.ComputeHash(byteValue);
    var hash = Convert.ToBase64String(byteHash);
    return GenerateHash(hash, salt, iterator - 1);
  }

  public string GenerateSalt()
  {
    using var rng = RandomNumberGenerator.Create();
    var byteSalt = new byte[16];
    rng.GetBytes(byteSalt);
    var salt = Convert.ToBase64String(byteSalt);
    return salt;
  }
}

public interface IPasswordHasher
{
  string GenerateHash(string p, string salt, int iterator);
  string GenerateSalt();
}