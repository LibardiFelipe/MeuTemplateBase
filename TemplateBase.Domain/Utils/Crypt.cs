using System;
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

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(text);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(array).Replace("/", "_");
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
                byte[] buffer = Convert.FromBase64String(text.Replace("_", "/"));

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
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
