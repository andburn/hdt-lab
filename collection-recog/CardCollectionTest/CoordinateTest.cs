using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace AndBurn.HDT.Plugins.CardCollection
{
    [TestClass]
    public class CoordinateTest
    {
        [TestMethod]
        public void TestCardRegionDefaultAdd()
        {
            var cr = new CardRegion();
            var expected = new CardRegion(0, 0, 3, 4);
            Assert.AreEqual<CardRegion>(expected, cr.Add(3, 4));
        }

        [TestMethod]
        public void TestCardRegionAdd()
        {
            var cr = new CardRegion(20, 30, 164, 80);
            var expected = new CardRegion(20, 30, 100, -6);
            Assert.AreEqual<CardRegion>(expected, cr.Add(-64, -86));
        }

        [TestMethod]
        public void TestRegionPixelCoordinatesAt1080_1()
        {
            CardPositions cp = new CardPositions(1920, 1080);
            PointF pf = new PointF(0.039F, 0.169F);
            Point p = new Point(296, 183);
            Assert.AreEqual(p, cp.ToPixelCoordinates(pf));
        }

        [TestMethod]
        public void TestRegionPixelCoordinatesAt1080_2()
        {
            CardPositions cp = new CardPositions(1920, 1080);
            PointF pf = new PointF(0.043F, 0.285F);
            Point p = new Point(302, 308);
            Assert.AreEqual(p, cp.ToPixelCoordinates(pf));
        }

        [TestMethod]
        public void TestRegionPosition()
        {
            CardPositions cp = new CardPositions(1920, 1080);
            CardRegion expected = new CardRegion(0, 0, 384, 489);
            Assert.AreEqual(expected, cp.Get(0, RegionType.Count));
        }

        [TestMethod]
        public void TestRegionPositionMana1()
        {
            CardPositions cp = new CardPositions(1920, 1080);
            CardRegion expected = new CardRegion(40, 40, 296, 183);
            Assert.AreEqual(expected, cp.Get(0, RegionType.Mana));
        }

        [TestMethod]
        public void TestRegionPositionMana2()
        {
            CardPositions cp = new CardPositions(1920, 1080);
            CardRegion expected = new CardRegion(40, 40, 537, 183);
            Assert.AreEqual(expected, cp.Get(1, RegionType.Mana));
        }
    }
}
