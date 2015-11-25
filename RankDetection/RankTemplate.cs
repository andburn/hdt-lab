using me.andburn.DDSReader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Hearthstone
{
    public static class RankTemplate
    {
        // Create the template images from the full textures.
        //    Uses extracted 'Medal_Ranked_X.dds' textures from
        //    'textures0.unity3d' and 'shared0.unity3d'
        public static void Create(string inDir, string outDir)
        {
            if (Directory.Exists(inDir) == false)
                return;
            if (Directory.Exists(outDir) == false)
                Directory.CreateDirectory(outDir);           

            var files = Directory.GetFiles(inDir);
            foreach (var f in files)
            {
                Regex re = new Regex(@"Ranked_(\d+)\.dds");
                Match m = re.Match(f);
                if (m.Success)
                {
                    var rank = m.Groups[1].Captures[0].Value;

                    byte[] data = File.ReadAllBytes(f);

                    Bitmap bmp = DDSReader.LoadImage(data);
                    bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    Bitmap scaled = new Bitmap(bmp, new System.Drawing.Size(70, 70));
                    // TODO: need grahics settings etc...
                    System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(23, 23, 24, 24);

                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height, PixelFormat.Format24bppRgb);

                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.DrawImage(scaled, new System.Drawing.Rectangle(0, 0, target.Width, target.Height),
                                         cropRect,
                                         GraphicsUnit.Pixel);
                    }

                    target.Save(Path.Combine(outDir, rank + ".bmp"));
                }
            }           
        }
    }
}
