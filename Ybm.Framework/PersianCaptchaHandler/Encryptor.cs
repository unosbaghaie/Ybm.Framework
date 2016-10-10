using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ybm.Framework.PersianCaptchaHandler
{
    public class Encryptor
    {
        private static string Password
        {
            get
            {
                return "Yepass";
            }
        }

        private static string Salt
        {
            get
            {
                return "TheSalt";
            }
        }

        public static string Encrypt(string clearText)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Encryptor.Password, Encoding.Unicode.GetBytes(Encryptor.Salt));
            MemoryStream memoryStream = new MemoryStream();
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = passwordDeriveBytes.GetBytes(32);
            rijndael.IV = passwordDeriveBytes.GetBytes(16);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.Close();
            byte[] inArray = memoryStream.ToArray();
            return Convert.ToBase64String(inArray);
        }

        public static string Decrypt(string cipherText)
        {
            byte[] array = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Encryptor.Password, Encoding.Unicode.GetBytes(Encryptor.Salt));
            MemoryStream memoryStream = new MemoryStream();
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = passwordDeriveBytes.GetBytes(32);
            rijndael.IV = passwordDeriveBytes.GetBytes(16);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.Close();
            byte[] bytes = memoryStream.ToArray();
            return Encoding.Unicode.GetString(bytes);
        }
    }
}
