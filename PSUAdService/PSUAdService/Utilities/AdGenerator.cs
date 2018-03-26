using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DrawingCore;
using System.IO;

namespace PSUAdService.Utilities
{
    public class AdGenerator
    {
        public static string GenerateAd()
        {            
            int width = 500;
            int height = 50;
            Bitmap bmp = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bmp))
            {
                var brushncolor = GetRandomBrush();
                Brush brush = brushncolor.Item1;
                Rectangle r = new Rectangle(0, 0, width-2, height-2);
                graphics.FillRectangle(brush, r);
                graphics.DrawRectangle(Pens.Black, r);
                string phrase = GetAdPhrase();
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                graphics.DrawString(phrase, new Font("Comic Sans", 12f), brushncolor.Item2, r, drawFormat);
                
            }
            MemoryStream stream = new MemoryStream();
            bmp.Save(stream, System.DrawingCore.Imaging.ImageFormat.Jpeg);
            //format from stream to byte[]
            byte[] data = stream.ToArray();
            stream.Write(data, 0, data.Length);
            return Convert.ToBase64String(data);
        }

        private static (Brush, Brush) GetRandomBrush()
        {
            Random r = new Random();
            int red = r.Next(0, byte.MaxValue + 1);
            int green = r.Next(0, byte.MaxValue + 1);
            int blue = r.Next(0, byte.MaxValue + 1);
            Color c = Color.FromArgb(red, green, blue);
            Brush brush = new SolidBrush(c);
            Color ic = Color.FromArgb(c.ToArgb() ^ 0xffffff);
            Brush txtbrush = new SolidBrush(ic);
            return (brush, txtbrush);
        }

        private static string GetAdPhrase()
        {
            List<string> phrases = new List<string>();
            phrases.Add("Wine Gums on sale, get yours today");
            phrases.Add("Coca Cola - share it with a friend");
            Random r = new Random();
            int index = r.Next(phrases.Count);
            return phrases[index];
        }
    }
}
