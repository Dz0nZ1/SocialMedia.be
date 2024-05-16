using System.Security.Cryptography;
using System.Text;

namespace SocialMedia.Application.Common.Extensions;

public static class AesEncryptionExtensions
{

    public static string Encrypt(this string text, string key)
    {

        byte[] iv = new byte[16];
        byte[] array;


        using (Aes aes = Aes.Create())
        {
            aes.Key = key.GenerateSha256();
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }

                    array = memoryStream.ToArray();
                }
            }
            
        }
        
        
        return Convert.ToBase64String(array);
    }


    public static byte[] GenerateSha256(this string text) => SHA256.HashData(Encoding.UTF8.GetBytes(text));
    
    
    

    public static string Decrypt(this string text, string key)
    {

        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(text);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key.GenerateSha256();
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
        
    }
    
    
}