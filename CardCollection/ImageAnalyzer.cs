using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndBurn.HDT.Plugins.CardCollection
{
    public class ImageAnalyzer
    {
        public static CardCount Recognize(string file, bool legend = false)
        {
            CardCount count = new CardCount();
            CardPositions pos = new CardPositions(1920, 1080);
            try
            {
                // full screen capture
                var bmp = new Bitmap(file);
                // 1 - Check for existence at position 0
                var exists1 = ReadRegion(bmp, pos.Get(0, RegionType.Mana), 90, 360, 0, 1);
                if (exists1)
                {
                    var count1 = ReadRegion(bmp, pos.Get(0, RegionType.Count), 0, 40, 0, 0.4);
                    // 2 - Check for existence at position 1
                    var exists2 = ReadRegion(bmp, pos.Get(1, RegionType.Mana), 90, 360, 0, 1);
                    if (exists2)
                    {
                        var count2 = ReadRegion(bmp, pos.Get(1, RegionType.Count), 0, 40, 0, 0.4);
                        // 2a - true => standard & gold (TODO: special cases)
                        count.Standard = count1 ? 2 : 1;
                        count.Golden = count2 ? 2 : 1;
                    }
                    else
                    {
                        // 2b - false => position 0 may be gold, check it
                        var goldRegion = pos.Get(0, RegionType.Gold);
                        if (legend)
                            goldRegion = pos.Get(0, RegionType.GoldLegend);
                        var gold1 = ReadRegion(bmp, goldRegion, 40, 57, 0.55, 1);
                        if (gold1)
                        {
                            count.Golden = count1 ? 2 : 1;
                        }
                        else
                        {
                            count.Standard = count1 ? 2 : 1;
                        }
                    }
                }
                else
                {
                    // no matches
                    return count;
                }                
    
                
                bmp.Dispose(); // TODO: add finally
            }
            catch (FileNotFoundException e)
            {
                Logger.Write("Exception: " + e, "Recognize");
            }            
            return count;
        }

        private static bool ReadRegion(Bitmap bmp, CardRegion region,
            double minHue, double maxHue, double minBrightness, double maxBrightness)
        {
            //var full = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            if (region.Size.IsEmpty)
            {
                return ReadPixel(bmp, region, minHue, maxHue, minBrightness, maxBrightness);
            }
            var regionBitmap = bmp.Clone(
                new Rectangle(region.Point, region.Size), PixelFormat.Format32bppArgb);
            // TEMP
            //regionBitmap.Save(region.Size.Width + minHue + ".bmp", ImageFormat.Bmp);
            var hb = GetAverageHueAndBrightness(regionBitmap);
            var hue = hb.Hue;
            var brightness = hb.Brightness;
            Console.Write("[{0} Hue:{1} Bri:{2}]\n", region.Size, Math.Round(hue, 4), Math.Round(brightness, 4));
            regionBitmap.Dispose();
            return (hue >= minHue && hue <= maxHue) && (brightness >= minBrightness && brightness <= maxBrightness);
        }

        private static bool ReadPixel(Bitmap bmp, CardRegion region,
            double minHue, double maxHue, double minBrightness, double maxBrightness)
        {
            var pixel = bmp.GetPixel(region.Point.X + 16, region.Point.Y + 16);
            var hue = Math.Round(pixel.GetHue());
            Console.Write("[50x30 Hue:{0} Bri:{1}\n", pixel.GetHue(), pixel.GetBrightness());
            return pixel.GetHue() < 40.0 && pixel.GetBrightness() < 0.4;
        }

        private static HueAndBrightness GetAverageHueAndBrightness(Bitmap bmp, double saturationThreshold = 0.05)
        {
            var totalHue = 0.0f;
            var totalBrightness = 0.0f;
            var validPixels = 0;
            for (var i = 0; i < bmp.Width; i++)
            {
                for (var j = 0; j < bmp.Height; j++)
                {
                    var pixel = bmp.GetPixel(i, j);

                    //ignore sparkle
                    if (pixel.GetSaturation() > saturationThreshold)
                    {
                        totalHue += pixel.GetHue();
                        totalBrightness += pixel.GetBrightness();
                        validPixels++;
                    }
                }
            }

            return new HueAndBrightness(totalHue / validPixels, totalBrightness / validPixels);
        }

        private class HueAndBrightness
        {
            public HueAndBrightness(double hue, double brightness)
            {
                Hue = hue;
                Brightness = brightness;
            }

            public double Hue { get; private set; }
            public double Brightness { get; private set; }
        }
    }
}
