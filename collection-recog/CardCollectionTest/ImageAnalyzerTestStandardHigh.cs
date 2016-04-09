using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndBurn.HDT.Plugins.CardCollection
{
    /*
     * Image tests at 1440x900
     */
    [TestClass]
    public class ImageAnalyzerTestStandardHigh
    {
        private const string Dir = @"Files\1440x900\";

        [TestMethod]
        public void TestImage_NoMatches()
        {
            Assert.AreEqual(new CardCount(),
                ImageAnalyzer.Recognize(Dir + "no_matches.png"));
        }

        [TestMethod]
        public void TestImage_Standard_1_0()
        {
            Assert.AreEqual(new CardCount(1, 0),
                ImageAnalyzer.Recognize(Dir + "standard_1_0.png"));
        }

        [TestMethod]
        public void TestImage_Standard_2_0()
        {
            Assert.AreEqual(new CardCount(2, 0),
                ImageAnalyzer.Recognize(Dir + "standard_2_0.png"));
        }

        [TestMethod]
        public void TestImage_Standard_2_1()
        {
            Assert.AreEqual(new CardCount(2, 1),
                ImageAnalyzer.Recognize(Dir + "standard_2_1.png"));
        }

        [TestMethod]
        public void TestImage_Standard_2_2()
        {
            Assert.AreEqual(new CardCount(2, 2),
                ImageAnalyzer.Recognize(Dir + "standard_2_2.png"));
        }

        [TestMethod]
        public void TestImage_Legend_1_0()
        {
            Assert.AreEqual(new CardCount(1, 0),
                ImageAnalyzer.Recognize(Dir + "legend_1_0.png", true));
        }

        [TestMethod]
        public void TestImage_Legend_0_1()
        {
            Assert.AreEqual(new CardCount(0, 1),
                ImageAnalyzer.Recognize(Dir + "legend_0_1.png", true));
        }
    }
}
