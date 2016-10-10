using System;
using System.Security.Cryptography;

namespace Ybm.Framework.PersianCaptchaHandler
{
    public class RandomGenerator
    {
        private static readonly byte[] Randb = new byte[4];

        private static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        public static int Next()
        {
            RandomGenerator.Rand.GetBytes(RandomGenerator.Randb);
            int num = BitConverter.ToInt32(RandomGenerator.Randb, 0);
            bool flag = num < 0;
            if (flag)
            {
                num = -num;
            }
            return num;
        }

        public static int Next(int max)
        {
            RandomGenerator.Rand.GetBytes(RandomGenerator.Randb);
            int num = BitConverter.ToInt32(RandomGenerator.Randb, 0);
            num %= max + 1;
            bool flag = num < 0;
            if (flag)
            {
                num = -num;
            }
            return num;
        }

        public static int Next(int min, int max)
        {
            return RandomGenerator.Next(max - min) + min;
        }
    }
}
