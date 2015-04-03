using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndBurn.HDT.Plugins.CardCollection
{
    [TestClass]
    public class CardCollectionTest
    {
        [TestMethod]
        public void TestGetCardListFromDB()
        {
            int count = CollectionExporter.GetCardList().Count;
            Assert.AreEqual(535, count);
        }

        [TestMethod]
        public void TestGetCardListFromFile()
        {
            int count = CollectionExporter.GetCardList(@"Files\card_names_small_list.txt").Count;
            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void TestGetCardListFromFileAreFound()
        {
            var cards = CollectionExporter.GetCardList(@"Files\card_names_single.txt");
            Assert.AreNotEqual("UNKNOWN", cards[0].Id);
        }
    }
}
