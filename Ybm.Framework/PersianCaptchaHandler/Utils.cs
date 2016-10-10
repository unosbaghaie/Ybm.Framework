using System;
using System.Text.RegularExpressions;

namespace Ybm.Framework.PersianCaptchaHandler
{
    public class Utils
    {
        private static readonly Regex NumberMatch = new Regex("^([0-9]*|\\d*\\.\\d{1}?\\d*)$", RegexOptions.Compiled);

        public static bool IsNumber(string number2Match)
        {
            return Utils.NumberMatch.IsMatch(number2Match);
        }
    }
}
