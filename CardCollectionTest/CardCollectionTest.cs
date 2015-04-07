using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AndBurn.HDT.Plugins.CardCollection
{
    [TestClass]
    public class CardCollectionTest
    {
        [TestMethod]
        public void TestGetCardListFromDB()
        {
            int count = CollectionExporter.GetCardList().Count;
            // After BRM 2015.03.03
            Assert.AreEqual(535, count);
        }

        [TestMethod]
        public void TestGetCardListFromFile()
        {
            int count = CollectionExporter.GetCardList(@"Files\card_names_small_list.txt").Count;
            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void TestListFileNotFoundHandled()
        {
            try
            {
                CollectionExporter.GetCardList(@"fake/file.txt");
            } 
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got: " + e.GetType());
            }
        }

        [TestMethod]
        public void TestGetCardListFromFileAreFound()
        {
            // Test on Lorewalker Cho ID:EX1_100, Not found == UNKNOWN
            var cards = CollectionExporter.GetCardList(@"Files\card_names_single.txt");
            Assert.AreEqual("EX1_100", cards[0].Id);
        }

        [TestMethod]
        public void TestImageNoMatches()
        {
            Assert.AreEqual(new CardCount(), 
                ImageAnalyzer.Recognize(@"Files\no_matches.bmp"));
        }

        [TestMethod]
        public void TestImageStandardByOne()
        {
            Assert.AreEqual(new CardCount(1, 0), 
                ImageAnalyzer.Recognize(@"Files\standard_1_0.bmp"));
        }

        [TestMethod]
        public void TestImageStandardByTwo()
        {
            Assert.AreEqual(new CardCount(2, 0), 
                ImageAnalyzer.Recognize(@"Files\standard_2_0.bmp"));
        }

        [TestMethod]
        public void TestImageStandardByOneGoldByOne()
        {
            Assert.AreEqual(new CardCount(1, 1), 
                ImageAnalyzer.Recognize(@"Files\standard_1_1.bmp"));
        }

        [TestMethod]
        public void TestImageStandardByTwoGoldByOne()
        {
            Assert.AreEqual(new CardCount(2, 1), 
                ImageAnalyzer.Recognize(@"Files\standard_2_1.bmp"));
        }

        [TestMethod]
        public void TestImageGoldByOne()
        {
            Assert.AreEqual(new CardCount(0, 1), 
                ImageAnalyzer.Recognize(@"Files\standard_0_1.bmp"));
        }

        [TestMethod]
        public void TestImageStandardByTwoGoldByTwo()
        {
            Assert.AreEqual(new CardCount(2, 2),
                ImageAnalyzer.Recognize(@"Files\standard_2_2.bmp"));
        }

        [TestMethod]
        public void TestImageLegendByOne()
        {
            Assert.AreEqual(new CardCount(1, 0),
                ImageAnalyzer.Recognize(@"Files\legend_1_0.bmp", true));
        }

        [TestMethod]
        public void TestImageLegendGoldByOne()
        {
            Assert.AreEqual(new CardCount(0, 1),
                ImageAnalyzer.Recognize(@"Files\legend_0_1.bmp", true));
        }
    }
}
