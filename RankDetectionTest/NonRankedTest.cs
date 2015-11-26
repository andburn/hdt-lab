using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hearthstone.Ranked;
using System.Drawing;

namespace RankDetectionTest
{
    [TestClass]
    public class NonRankedTest
    {
        [TestMethod]
        public void NonRankedGame_IsNotSuccessful_hi()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsGameAiHi.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void NonRankedGame_IsNotSuccessful_lo()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsGameAiLo.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void MenuA_IsNotSuccessful_hi()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsMenuAHi.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void MenuA_IsNotSuccessful_lo()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsMenuALo.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void MenuB_IsNotSuccessful_hi()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsMenuBHi.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void MenuB_IsNotSuccessful_lo()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsMenuBLo.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void NonRankedFriendsList_IsNotSuccessful_hi()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsGameAiFriendsHi.png"));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void NonRankedFriendsList_IsNotSuccessful_lo()
        {
            var result = RankDetection.Match(new Bitmap(@"data\HsGameAiFriendsLo.png"));
            Assert.IsFalse(result.Success);
        }

    }
}
