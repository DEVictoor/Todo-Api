using System.Security.Cryptography;
using System.Text;

namespace todoapi;

public class HashingAndSaltingPassword : IHashingAndSaltingPassword
{
  const int keySize = 64;
  const int iterations = 350000;

  HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

  public string HashPassword(string password, out byte[] salt)
  {
    salt = RandomNumberGenerator.GetBytes(keySize);

    var hash = Rfc2898DeriveBytes.Pbkdf2(
      Encoding.UTF8.GetBytes(password),
      salt,
      iterations,
      hashAlgorithm,
      keySize
    );
    return Convert.ToHexString(hash);
  }

  public bool VerifyPassword(string password, string hash, byte[] salt)
  {
    var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
    return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
  }
}

public interface IHashingAndSaltingPassword
{
  string HashPassword(string password, out byte[] salt);
  bool VerifyPassword(string password, string hash, byte[] salt);
}