using System.Security.Cryptography;
using System.Text;

namespace todoapi.Helpers;

public class HashingStrings : IHashingStrings
{
  public string GenerarHash(string password)
  {
    using (var sha256 = SHA256.Create())
    {
      byte[] bytesContraseña = Encoding.UTF8.GetBytes(password);
      byte[] hashBytes = sha256.ComputeHash(bytesContraseña);
      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < hashBytes.Length; i++)
      {
        builder.Append(hashBytes[i].ToString("x2"));
      }
      return builder.ToString(); ;
    }
  }

  public bool Verificar(string password, string hash)
  {
    string hashIngresado = GenerarHash(password);
    return string.Equals(hashIngresado, hash, StringComparison.OrdinalIgnoreCase);
  }
}
public interface IHashingStrings
{
  string GenerarHash(string password);
  bool Verificar(string password, string hash);
}