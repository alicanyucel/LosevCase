using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    private readonly byte[] key = Encoding.UTF8.GetBytes("SuperSecretKey12"); 
    private readonly byte[] iv = Encoding.UTF8.GetBytes("InitializationVe");  

    public string HashPassword(TUser user, string password)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using var swEncrypt = new StreamWriter(csEncrypt);

            swEncrypt.Write(password);
            swEncrypt.Flush();
            csEncrypt.FlushFinalBlock();

            return Convert.ToBase64String(msEncrypt.ToArray());
        }
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
    {
        string decryptedPassword;

        try
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using var msDecrypt = new MemoryStream(Convert.FromBase64String(hashedPassword));
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                decryptedPassword = srDecrypt.ReadToEnd();
            }
        }
        catch
        {
            return PasswordVerificationResult.Failed;
        }

        return decryptedPassword == providedPassword ? PasswordVerificationResult.Success: PasswordVerificationResult.Failed;
    }
}
