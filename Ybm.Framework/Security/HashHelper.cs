using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Ybm.Framework.Security
{
    public class HashHelper
    {
        public byte[] GetUserHashKey(string password, string userName, out string hasedStr)
        {
            return Hash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(userName), out  hasedStr);
        }
        private byte[] Hash(byte[] value, byte[] salt,out string hasedStr)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();
            value.CopyTo(saltedValue, 0);
            salt.CopyTo(saltedValue, value.Length);
            var hashed = new SHA256Managed().ComputeHash(saltedValue);
            hasedStr = Encoding.UTF8.GetString(hashed);
            return hashed;
        }
        public byte[] GetByteFromHashed(string hashed)
        {
            return  Encoding.UTF8.GetBytes(hashed);
        }
    }
}

