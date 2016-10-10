using System;

namespace Ybm.Framework.PersianCaptchaHandler
{
    public class NumberToString
    {
        private static readonly string[] Yakan = new string[]
        {
            "صفر",
            "یک",
            "دو",
            "سه",
            "چهار",
            "پنج",
            "شش",
            "هفت",
            "هشت",
            "نه"
        };

        private static readonly string[] Dahgan = new string[]
        {
            "",
            "",
            "بیست",
            "سی",
            "چهل",
            "پنجاه",
            "شصت",
            "هفتاد",
            "هشتاد",
            "نود"
        };

        private static readonly string[] Dahyek = new string[]
        {
            "ده",
            "یازده",
            "دوازده",
            "سیزده",
            "چهارده",
            "پانزده",
            "شانزده",
            "هفده",
            "هجده",
            "نوزده"
        };

        private static readonly string[] Sadgan = new string[]
        {
            "",
            "یکصد",
            "دوصد",
            "سیصد",
            "چهارصد",
            "پانصد",
            "ششصد",
            "هفتصد",
            "هشتصد",
            "نهصد"
        };

        private static readonly string[] Basex = new string[]
        {
            "",
            "هزار",
            "میلیون",
            "میلیارد",
            "تریلیون"
        };

        private static string Getnum3(int num3)
        {
            string text = "";
            int num4 = num3 % 100;
            int num5 = num3 / 100;
            bool flag = num5 != 0;
            if (flag)
            {
                text = NumberToString.Sadgan[num5] + " و ";
            }
            bool flag2 = num4 >= 10 && num4 <= 19;
            if (flag2)
            {
                text += NumberToString.Dahyek[num4 - 10];
            }
            else
            {
                int num6 = num4 / 10;
                bool flag3 = num6 != 0;
                if (flag3)
                {
                    text = text + NumberToString.Dahgan[num6] + " و ";
                }
                int num7 = num4 % 10;
                bool flag4 = num7 != 0;
                if (flag4)
                {
                    text = text + NumberToString.Yakan[num7] + " و ";
                }
                text = text.Substring(0, text.Length - 3);
            }
            return text;
        }

        public static string ConvertIntNumberToFarsiAlphabatic(string snum)
        {
            string text = "";
            bool flag = snum == "0";
            string result;
            if (flag)
            {
                result = NumberToString.Yakan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int num = snum.Length / 3 - 1;
                for (int i = 0; i <= num; i++)
                {
                    int num2 = int.Parse(snum.Substring(i * 3, 3));
                    bool flag2 = num2 != 0;
                    if (flag2)
                    {
                        text = string.Concat(new string[]
                        {
                            text,
                            NumberToString.Getnum3(num2),
                            " ",
                            NumberToString.Basex[num - i],
                            " و "
                        });
                    }
                }
                text = text.Substring(0, text.Length - 3);
                result = text;
            }
            return result;
        }
    }
}
