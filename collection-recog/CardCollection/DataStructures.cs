using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndBurn.HDT.Plugins.CardCollection
{
    public class CardPositions
    {
        private const double xOffset = 0.167;

        private const double ManaSize = 0.037;
        private const double GoldSize = 0.022;
        private const double CountSize = 0;

        private PointF ManaPoint = new PointF(0.039F, 0.169F);
        private PointF GoldPoint = new PointF(0.168F, 0.183F);
        private PointF GoldLegendPoint = new PointF(0.043F, 0.285F);
        private PointF CountPoint = new PointF(0.1F, 0.453F);

        private const double DimensionScale = 0.037;

        private int xPixelOffset;

        private CardRegion[] Mana;
        private CardRegion[] Gold;
        private CardRegion[] GoldLegend;
        private CardRegion[] Count;

        public double Width { get; private set; }
        public double Height { get; private set; }
        public double Ratio { get; private set; }

        public CardPositions(int width, int height)
        {
            Width = width;
            Height = height;
            Ratio = (4.0/3.0)/(Width/Height);
            Mana = CalculateAllPositions(ManaPoint, ManaSize);
            Gold = CalculateAllPositions(GoldPoint, GoldSize);
            GoldLegend = CalculateAllPositions(GoldLegendPoint, GoldSize);
            Count = CalculateAllPositions(CountPoint, CountSize);
        }

        private CardRegion[] CalculateAllPositions(PointF p, double d)
        {
            CardRegion[] regions = new CardRegion[4];
            // calculate the scaled square size
            var dim = (int)Math.Round(Height * d);
            // top row (0-3)
            for (var i = 0; i < 4; i++)
            {
                var tmp = new PointF((float)(p.X + i * xOffset), p.Y);
                regions[i] = new CardRegion(ToPixelCoordinates(tmp), new Size(dim, dim));
            }
            // bottom row (4-7), may be unecessary
            return regions;
        }

        public Point ToPixelCoordinates(PointF f)
        {
            double x = (Width * Ratio * f.X) + (Width * (1 - Ratio) / 2);
            return new Point((int) Math.Round(x), (int) Math.Round(f.Y * Height));
        }

        public CardRegion Get(int position, RegionType region)
        {
            switch (region)
            {
                case RegionType.Mana:
                    return Mana[position];
                case RegionType.Gold:
                    return Gold[position];
                case RegionType.GoldLegend:
                    return GoldLegend[position];
                case RegionType.Count:
                    return Count[position];
                default:
                    return new CardRegion();
            }
        }
    }

    public enum RegionType
    {
        Mana, Gold, GoldLegend, Count
    }

    public class CardRegion
    {
        public Point Point { get; private set; }
        public Size Size { get; private set; }

        public CardRegion()
        {
            Point = new Point(0, 0);
            Size = new Size(0, 0);
        }

        public CardRegion(int width, int height, int x, int y)
        {
            Point = new Point(x, y);
            Size = new Size(width, height);
        }

        public CardRegion(Point p, Size s)
        {
            Point = new Point(p.X, p.Y);
            Size = new Size(s.Width, s.Height);
        }

        public CardRegion Add(int x, int y) {
            return new CardRegion(Size.Width, Size.Height, Point.X + x, Point.Y + y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            CardRegion cr = obj as CardRegion;
            if ((object)cr == null)
            {
                return false;
            }
            return (Point == cr.Point) && (Size == cr.Size);
        }

        public bool Equals(CardRegion cr)
        {
            if ((object)cr == null)
            {
                return false;
            }

            return (Point == cr.Point) && (Size == cr.Size);
        }

        public override int GetHashCode()
        {
            return Point.X ^ Point.Y ^ Size.Width ^ Size.Height;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Point, Size);
        }

    }

    public class CardCount
    {
        public int Standard { get; set; }
        public int Golden { get; set; }

        public CardCount()
        {
            Standard = 0;
            Golden = 0;
        }

        public CardCount(int standard, int golden)
        {
            Standard = standard;
            Golden = golden;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            CardCount cc = obj as CardCount;
            if ((object)cc == null)
            {
                return false;
            }
            return (Standard == cc.Standard) && (Golden == cc.Golden);
        }

        public bool Equals(CardCount cc)
        {
            if ((object)cc == null)
            {
                return false;
            }

            return (Standard == cc.Standard) && (Golden == cc.Golden);
        }

        public override int GetHashCode()
        {
            return Standard ^ Golden;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Standard, Golden);
        }
    }
}
