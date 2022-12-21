using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TemplateBase.Domain.Utils
{
    public static class Crypt
    {
        public static string EncryptString(string key, string text)
        {
            try
            {
                byte[] iv = new byte[16];
                byte[] array;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new())
                    {
                        using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new(cryptoStream))
                            {
                                streamWriter.Write(text);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }

                return WebEncoders.Base64UrlEncode(array);
            }
            catch
            {
                return "";
            }
        }

        public static string DecryptString(string key, string text)
        {
            try
            {
                byte[] iv = new byte[16];
                byte[] buffer = WebEncoders.Base64UrlDecode(text);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new(buffer))
                    {
                        using (CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
