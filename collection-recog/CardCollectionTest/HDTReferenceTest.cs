using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hearthstone_Deck_Tracker;

namespace CardCollectionTest
{
    [TestClass]
    public class HDTReferenceTest
    {
        [TestMethod]
        public void TestConfigValues()
        {
            Assert.AreEqual("enUS", Config.Instance.SelectedLanguage);
        }
    }
}
