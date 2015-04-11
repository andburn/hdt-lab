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

    }
}
