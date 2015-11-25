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
                // texture files are named 'Medal_Ranked_X.dds'
                Regex re = new Regex(@"Ranked_(\d+)\.dds");
                Match m = re.Match(f);
                if (m.Success)
                {
                    var rank = m.Groups[1].Captures[0].Value;

                    byte[] data = File.ReadAllBytes(f);
                    // convert the dds file to bitmap
                    Bitmap bmp = DDSReader.LoadImage(data);
                    // need flip HS textures
                    bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    // resize to 70x70
                    Bitmap scaled = new Bitmap(bmp, new System.Drawing.Size(70, 70));
                    // crop at (23,23) size 24x24
                    Rectangle cropRect = new Rectangle(23, 23, 24, 24);
                    // set output bitmap to 24bpp (for Aforge)
                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height, PixelFormat.Format24bppRgb);

                    using (Graphics g = Graphics.FromImage(target))
                    {
                        // set graphic quality settings
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.DrawImage(scaled, new Rectangle(0, 0, target.Width, target.Height),
                                         cropRect, GraphicsUnit.Pixel);
                    }

                    // save template to output directory as bitmap
                    target.Save(Path.Combine(outDir, rank + ".bmp"));
                }
            }           
        }
    }
}
