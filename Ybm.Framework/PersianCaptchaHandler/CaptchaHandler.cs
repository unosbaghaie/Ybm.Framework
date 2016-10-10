using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Web;

namespace Ybm.Framework.PersianCaptchaHandler
{
    public class CaptchaHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            bool flag = context.Request.Params["text"] == null;
            if (!flag)
            {
                string cipherText = context.Request.Params["text"];
                string text = Encryptor.Decrypt(cipherText);
                Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
                Font font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point);
                Graphics graphics = Graphics.FromImage(bitmap);
                int num = (int)graphics.MeasureString(text, font).Width + 10;
                int num2 = (int)graphics.MeasureString(text, font).Height + 10;
                int num3 = (155 - num) / 2;
                int num4 = (25 - num2) / 2;
                bitmap = new Bitmap(bitmap, new Size(155, 25));
                graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.DrawString(text, font, new SolidBrush(Color.Black), (float)num3, (float)num4);
                double num5 = (double)(RandomGenerator.Next(2, 5) * ((RandomGenerator.Next(5) == 1) ? 1 : -1));
                using (Bitmap bitmap2 = (Bitmap)bitmap.Clone())
                {
                    for (int i = 0; i < 25; i++)
                    {
                        for (int j = 0; j < 155; j++)
                        {
                            int num6 = (int)((double)j + num5 * Math.Sin(3.1415926535897931 * (double)i / 84.0));
                            int num7 = (int)((double)i + num5 * Math.Cos(3.1415926535897931 * (double)j / 44.0));
                            bool flag2 = num6 < 0 || num6 >= 155;
                            if (flag2)
                            {
                                num6 = 0;
                            }
                            bool flag3 = num7 < 0 || num7 >= 25;
                            if (flag3)
                            {
                                num7 = 0;
                            }
                            Color pixel = bitmap2.GetPixel(num6, num7);
                            bitmap.SetPixel(j, i, pixel);
                        }
                    }
                }
                context.Response.ContentType = "image/jpeg";
                bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            }
        }
    }
}
